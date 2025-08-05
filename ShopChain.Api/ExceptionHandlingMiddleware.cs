using ShopChain.Application.Commons;
using System.Data.Common;

namespace ShopChain.Api
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred. RequestId: {RequestId}", context.TraceIdentifier);

            var response = context.Response;
            response.ContentType = "application/json";

            var (statusCode, errorCode, errorMessage, errors) = ex switch
            {
                FluentValidation.ValidationException validationEx => (
                    400,
                    ErrorCodes.InvalidCredentials,
                    "Validation failed.",
                    validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                ),
                KeyNotFoundException => (
                    404,
                    ErrorCodes.UserNotFound,
                    "Resource not found.",
                    null
                ),
                ArgumentNullException => (
                    400,
                    ErrorCodes.InvalidCredentials,
                    "Invalid request data.",
                    null
                ),
                DbException => (
                    500,
                    ErrorCodes.SystemError,
                    "Database error occurred.",
                    null
                ),
                _ => (
                    500,
                    ErrorCodes.SystemError,
                    _env.IsDevelopment() ? ex.Message : "An unexpected error occurred.",
                    null
                )
            };

            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(new
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                Errors = errors,
                TraceId = context.TraceIdentifier // Include TraceId for debugging
            });
        }
    }
}