using System.Net;

using FluentValidation;

using TestProject.Domain.Responses;

namespace TestProject.Helpers
{
    public static class ValidationHelper
    {
        public static async Task<ValidationResponse> ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var result = new ValidationResponse();

            if (!(await validator.ValidateAsync(request)).IsValid)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Errors = (await validator
                    .ValidateAsync(request))
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .Aggregate((x, y) => $"{x}\n{y}");
            }

            return result;
        }
    }
}
