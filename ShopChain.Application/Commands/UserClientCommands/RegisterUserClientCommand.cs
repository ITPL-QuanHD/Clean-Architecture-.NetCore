using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ShopChain.Application.Commons;
using ShopChain.Application.Dtos;
using ShopChain.Core.Entities;
using ShopChain.Core.Interfaces;

namespace ShopChain.Application.Commands.UserClientCommands
{
    public class RegisterUserClientRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }

    public record RegisterUserClientCommand(RegisterUserClientRequest request) : IRequest<Result<UserClientDto>>;

    public class RegisterUserClientCommandValidator : AbstractValidator<RegisterUserClientCommand>
    {
        public RegisterUserClientCommandValidator()
        {
            RuleFor(x => x.request.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
                .Must(username => !string.IsNullOrWhiteSpace(username)).WithMessage("Username must not consist only of whitespace.");

            RuleFor(x => x.request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.request.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");
        }
    }

    public class RegisterUserClientCommandHandler : IRequestHandler<RegisterUserClientCommand, Result<UserClientDto>>
    {
        private readonly IUserClientRepository _userClientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserClientCommandHandler> _logger;
        private readonly IValidator<RegisterUserClientCommand> _validator;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserClientCommandHandler(
            IUserClientRepository userClientRepository,
            IMapper mapper,
            ILogger<RegisterUserClientCommandHandler> logger,
            IValidator<RegisterUserClientCommand> validator,
            IPasswordHasher passwordHasher)
        {
            _userClientRepository = userClientRepository ?? throw new ArgumentNullException(nameof(userClientRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<Result<UserClientDto>> Handle(RegisterUserClientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RegisterUserClientCommand for {Username}", request.request.Username);

            try
            {
                // 1. Input Validation
                var validationResult = _validator.Validate(request); // Use Validate instead of ValidateAsync
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    _logger.LogWarning("Validation failed for {Username}: {Errors}", request.request.Username, string.Join(" ", errors));
                    return Result<UserClientDto>.Failure(ErrorCodes.InvalidCredentials, errors);
                }

                // 2. Business Rules Validation
                var existingUser = await _userClientRepository.GetByUsernameAsync(request.request.Username, cancellationToken);
                if (existingUser != null)
                {
                    _logger.LogWarning("Username {Username} already exists", request.request.Username);
                    return Result<UserClientDto>.Failure(ErrorCodes.UsernameTaken, $"Username '{request.request.Username}' is already taken.");
                }

                // 3. Create Entity with Sanitized Input
                var userClient = new UserClient
                {
                    Username = SanitizeInput(request.request.Username),
                    PasswordHash = _passwordHasher.HashPassword(request.request.Password),
                    FullName = SanitizeInput(request.request.FullName),
                    Role = "User", // Consider moving to config if roles are dynamic
                    CreatedAt = DateTime.UtcNow
                };

                // 4. Save to Repository
                var savedUserClient = await _userClientRepository.RegisterAsync(userClient, cancellationToken);

                // 5. Map to DTO
                var userClientDto = _mapper.Map<UserClientDto>(savedUserClient);

                _logger.LogInformation("UserClient {Username} registered successfully", userClientDto.Username);

                return Result<UserClientDto>.Success(userClientDto)
                    .WithMetadata("CreatedAt", DateTime.UtcNow.ToString("o"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user {Username}", request.request.Username);
                return Result<UserClientDto>.Failure(ErrorCodes.SystemError, "An unexpected error occurred.");
            }
        }

        private string SanitizeInput(string input)
        {
            return System.Web.HttpUtility.HtmlEncode(input.Trim());
        }
    }
}