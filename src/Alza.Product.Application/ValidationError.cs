namespace Alza.Product.Application
{
    public sealed class ValidationError
    {
        public string Message { get; }

        public string Code { get; }

        public static ValidationError EntityNotFound { get; } = new("Entity wasn't found.", code: ValidationErrorCodes.NotFound);

        public static ValidationError InternalServerError { get; } = new("Internal server error.", code: ValidationErrorCodes.InternalError);

        private ValidationError(string message, string code)
        {
            Message = message;
            Code = code;
        }
    }

    public static class ValidationErrorCodes
    {
        public const string NotFound = "notfound";
        public const string InternalError = "internalError";
    }
}
