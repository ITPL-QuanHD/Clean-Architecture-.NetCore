using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopChain.Application.Commands.UserClientCommands;
using ShopChain.Application.Commons;
using ShopChain.Application.Dtos;

namespace ShopChain.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserClientController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserClientRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    ErrorCode = ErrorCodes.InvalidCredentials,
                    ErrorMessage = "Request body is required."
                });
            }

            var command = new RegisterUserClientCommand(request);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                var response = new { result.Value, result.Metadata };
                return CreatedAtAction(nameof(Register), new { username = result.Value!.Username }, response);
            }

            return result.ErrorCode switch
            {
                ErrorCodes.UsernameTaken => Conflict(new 
                { 
                    result.ErrorCode,
                    result.ErrorMessage 
                }),

                ErrorCodes.InvalidCredentials => BadRequest(new 
                { 
                    result.ErrorCode, 
                    result.ErrorMessage, 
                    result.Errors 
                }),

                ErrorCodes.SystemError => StatusCode(500, new 
                {
                    result.ErrorCode, 
                    result.ErrorMessage 
                }),

                _ => BadRequest(new 
                {
                    result.ErrorCode, 
                    result.ErrorMessage, 
                    result.Errors 
                })
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserClientRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra request cơ bản
            if (request == null)
            {
                return BadRequest(new
                {
                    ErrorCode = ErrorCodes.InvalidCredentials,
                    ErrorMessage = "Request body is required."
                });
            }

            // Tạo command và gửi qua MediatR
            var command = new LoginUserClientCommand(request);
            var result = await _mediator.Send(command, cancellationToken);

            // Xử lý kết quả
            if (result.IsSuccess)
            {
                // Trả về 200 OK với dữ liệu và metadata
                var response = new
                {
                    result.Value,
                    result.Metadata
                };
                return Ok(response);
            }

            // Xử lý lỗi
            return result.ErrorCode switch
            {
                ErrorCodes.UserNotFound => NotFound(new
                {
                    result.ErrorCode,
                    result.ErrorMessage
                }),
                ErrorCodes.InvalidCredentials => Unauthorized(new
                {
                    result.ErrorCode,
                    result.ErrorMessage,
                    Errors = result.Errors // Trả về danh sách lỗi nếu có
                }),
                ErrorCodes.SystemError => StatusCode(500, new
                {
                    result.ErrorCode,
                    result.ErrorMessage
                }),
                _ => BadRequest(new
                {
                    result.ErrorCode,
                    result.ErrorMessage,
                    Errors = result.Errors
                })
            };
        }
    }
}
