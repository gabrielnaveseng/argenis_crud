using Argenis.CRUD.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Argenis.CRUD.Borders.Shared
{
    public class UseCaseResponse<TResponse> where TResponse : class
    {
        public readonly UseCaseResponseKind Status;
        public readonly string ErrorMessage;
        public readonly IEnumerable<ErrorMessage> Errors;
        public readonly TResponse? Result;
        public readonly string ResultId;

        private UseCaseResponse(UseCaseResponseKind status, string errorMessage, IEnumerable<ErrorMessage> errors, TResponse? result, string resultId)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Errors = errors;
            Result = result;
            ResultId = resultId;
        }

        public static UseCaseResponse<TResponse> CreateSuccessResponse(TResponse result)
        {
            return SetStatus(UseCaseResponseKind.Success, string.Empty, new ErrorMessage[] { }, result, string.Empty);
        }
        public static UseCaseResponse<TResponse> CreateOkResponse(TResponse result)
        {
            return SetStatus(UseCaseResponseKind.OK, string.Empty, new ErrorMessage[] { }, result, string.Empty);
        }
        public static UseCaseResponse<TResponse> CreateNotFoundResponse(ErrorMessage error)
        {
            return SetStatus(UseCaseResponseKind.NotFound, "Data not found", new ErrorMessage[] { error }, null, string.Empty);
        }
        public static UseCaseResponse<TResponse> CreateInternalServerErrorResponse(ErrorMessage error)
        {
            return SetStatus(UseCaseResponseKind.InternalServerError, "Internal server error", new ErrorMessage[] { error }, null, string.Empty);
        }
        public static UseCaseResponse<TResponse> CreateBadRequestResponse(ErrorMessage error)
        {
            return SetStatus(UseCaseResponseKind.BadRequest, "Request is invalid", new ErrorMessage[] { error }, null, string.Empty);
        }

        public static UseCaseResponse<TResponse> CreateBadRequestResponse(IEnumerable<ErrorMessage> errors)
        {
            return SetStatus(UseCaseResponseKind.BadRequest, "Request is invalid", errors, null, string.Empty);
        }

        public bool Success()
        {
            return ErrorMessage == null || !ErrorMessage.Any();
        }

        private static UseCaseResponse<TResponse> SetStatus(UseCaseResponseKind status,
                                                            string errorMessage,
                                                            IEnumerable<ErrorMessage> errors,
                                                            TResponse? result,
                                                            string resultId)
        {
            return new UseCaseResponse<TResponse>(status, errorMessage, errors, result, resultId);
        }
    }
}