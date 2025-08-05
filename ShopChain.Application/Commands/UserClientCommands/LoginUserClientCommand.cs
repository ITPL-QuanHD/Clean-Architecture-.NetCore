using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ShopChain.Application.Commons;
using ShopChain.Application.Dtos;
using ShopChain.Core.Interfaces;

namespace ShopChain.Application.Commands.UserClientCommands
{
    public class LoginUserClientRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserClientCommandValidator : AbstractValidator<LoginUserClientCommand>
    {
        public LoginUserClientCommandValidator()
        {
            RuleFor(x => x.request.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }

    public record LoginUserClientCommand(LoginUserClientRequest request) : IRequest<Result<LoginResponseDto>>;

    public class LoginUserClientCommandHandler : IRequestHandler<LoginUserClientCommand, Result<LoginResponseDto>>
    {
        private readonly IUserClientRepository _userClientRepository;

        private readonly IPasswordHasher _passwordHasher;

        private readonly ILogger<LoginUserClientCommandHandler> _logger;

        private readonly IValidator<LoginUserClientCommand> _validator;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserClientCommandHandler(IUserClientRepository userClientRepository, IPasswordHasher passwordHasher, ILogger<LoginUserClientCommandHandler> logger, IValidator<LoginUserClientCommand> validator, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userClientRepository = userClientRepository ?? throw new ArgumentNullException(nameof(userClientRepository));

            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _validator = validator ?? throw new ArgumentNullException(nameof(validator));

            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        }

        public async Task<Result<LoginResponseDto>> Handle(LoginUserClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate input
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Result<LoginResponseDto>.Failure(ErrorCodes.InvalidCredentials, errors);
                }

                // Tìm user
                var user = await _userClientRepository.GetByUsernameAsync(request.request.Username, cancellationToken);
                if (user == null)
                    return Result<LoginResponseDto>.Failure(ErrorCodes.UserNotFound, "User does not exist.");

                // Kiểm tra password
                if (!_passwordHasher.VerifyPassword(request.request.Password, user.PasswordHash))
                    return Result<LoginResponseDto>.Failure(ErrorCodes.InvalidCredentials, "Invalid password.");

                // Sinh JWT
                var token = _jwtTokenGenerator.GenerateToken(user);

                // Mapping kết quả
                var dto = new LoginResponseDto
                {
                    Username = user.Username,
                    FullName = user.FullName,
                    Token = token
                };

                return Result<LoginResponseDto>.Success(dto)
                    .WithMetadata("LoginTime", DateTime.UtcNow.ToString("o"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user {Username}", request.request.Username);
                return Result<LoginResponseDto>.Failure(ErrorCodes.SystemError, "An unexpected error occurred.");
            }
        }
    }
}
