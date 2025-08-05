namespace ShopChain.Application.Commons
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? ErrorCode { get; private set; }
        public IReadOnlyList<string>? Errors { get; private set; } // Hỗ trợ nhiều lỗi
        public Dictionary<string, object> Metadata { get; } = new();

        private Result(bool isSuccess, T? value, string? errorMessage, string? errorCode, IReadOnlyList<string>? errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Errors = errors;
        }

        // Success
        public static Result<T> Success(T value) => new(true, value, null, null, null);

        // Failure với một lỗi
        public static Result<T> Failure(string errorCode, string errorMessage)
            => new(false, default, errorMessage, errorCode, null);

        // Failure với nhiều lỗi (e.g., từ validation)
        public static Result<T> Failure(string errorCode, IReadOnlyList<string> errors)
            => new(false, default, string.Join(" ", errors), errorCode, errors);

        // Failure đơn giản (backward compatibility)
        public static Result<T> Failure(string errorMessage)
            => new(false, default, errorMessage, null, null);

        // Implicit conversion
        public static implicit operator Result<T>(T value) => Success(value);

        // Helper để thêm metadata
        public Result<T> WithMetadata(string key, object value)
        {
            Metadata[key] = value;
            return this;
        }
    }

    // Định nghĩa ErrorCodes
    public static class ErrorCodes
    {
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string SystemError = "SYSTEM_ERROR";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string UsernameTaken = "USERNAME_TAKEN";
    }
}