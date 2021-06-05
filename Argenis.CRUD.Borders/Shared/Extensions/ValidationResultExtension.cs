using FluentValidation.Results;

namespace Argenis.CRUD.Borders.Shared.Extensions
{
    public static class ValidationResultExtension
    {
        public static void AddError(this ValidationResult validationResult, string propertyName, string message, string errorCode) =>
            validationResult.Errors.Add(new ValidationFailure(propertyName, message) { ErrorCode = errorCode });
    }
}
