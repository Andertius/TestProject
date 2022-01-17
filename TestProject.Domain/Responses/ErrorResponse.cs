using System.Net;

namespace TestProject.Domain.Responses
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
