using System.Net;

namespace TestProject.Domain.Responses
{
    public class ValidationResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Errors { get; set; }
    }
}
