using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Argenis.CRUD.Borders.Shared;
using Argenis.CRUD.Shared.Models;
using Serilog;
using System;
using System.Linq;
using System.Net;

namespace Argenis.CRUD.Api.Models
{
    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            path = accessor.HttpContext.Request.Path.Value;
        }
        public IActionResult Convert<Tin>(UseCaseResponse<Tin> response) where Tin : class
        {
            return Convert<Tin, Tin>(response);
        }

        public IActionResult Convert<Tin, Tout>(UseCaseResponse<Tin> response, Func<Tin?, Tout?>? converter = null)
            where Tin : class
            where Tout : class
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);

            if (response.Success())
            {
                if (converter is null)
                    return BuildSuccessResult(response.Result, response.ResultId, response.Status);
                else
                    return BuildSuccessResult(converter.Invoke(response.Result), response.ResultId, response.Status);
            }
            else
            {
                var hasErrors = !response.Errors.Any();
                var errorResult = hasErrors
                    ? new[] { new ErrorMessage("000", response.ErrorMessage ?? "Unknown error") }
                    : response.Errors;

                return BuildError(errorResult, response.Status);
            }
        }

        private IActionResult BuildSuccessResult(object? data, string id, UseCaseResponseKind status)
        {
            return status switch
            {
                UseCaseResponseKind.DataPersisted => new CreatedResult($"{path}/{id}", data),
                UseCaseResponseKind.NonContent => new NoContentResult(),
                UseCaseResponseKind.DataAccepted => new AcceptedResult($"{path}/{id}", data),
                _ => new OkObjectResult(data),
            };
        }

        private ObjectResult BuildError(object data, UseCaseResponseKind status)
        {
            var httpStatus = GetErrorHttpStatusCode(status);
            if (httpStatus == HttpStatusCode.InternalServerError)
            {
                Log.Error($"[ERROR] {path} ({{@data}})", data);
            }

            return new ObjectResult(data)
            {
                StatusCode = (int)httpStatus
            };
        }

        private HttpStatusCode GetErrorHttpStatusCode(UseCaseResponseKind status)
        {
            switch (status)
            {
                case UseCaseResponseKind.RequestValidationError:
                case UseCaseResponseKind.ForeignKeyViolationError:
                case UseCaseResponseKind.BadRequest:
                    return HttpStatusCode.BadRequest;
                case UseCaseResponseKind.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case UseCaseResponseKind.Forbidden:
                    return HttpStatusCode.Forbidden;
                case UseCaseResponseKind.NotFound:
                    return HttpStatusCode.NotFound;
                case UseCaseResponseKind.UniqueViolationError:
                    return HttpStatusCode.Conflict;
                case UseCaseResponseKind.Unavailable:
                    return HttpStatusCode.ServiceUnavailable;
                case UseCaseResponseKind.BadGateway:
                    return HttpStatusCode.BadGateway;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
