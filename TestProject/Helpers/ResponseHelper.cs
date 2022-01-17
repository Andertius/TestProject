using Microsoft.AspNetCore.Mvc;

using TestProject.Domain.Responses;

namespace TestProject.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult ReturnResponse<T>(OperationResponse<T> response) where T : class
        {
            return response.Result switch
            {
                OperationResult.Failure => new BadRequestObjectResult(response.Error),
                OperationResult.NotFound => new NotFoundObjectResult(response.Error),
                OperationResult.Success => new OkObjectResult(response.Model),
                _ => throw new NotSupportedException(),
            };
        }
    }
}
