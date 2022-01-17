using System.Net;

using Microsoft.AspNetCore.Mvc;

using TestProject.Application.Services;
using TestProject.Application.Validators;
using TestProject.Domain.Requests;
using TestProject.Helpers;

namespace TestProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("contacts/create")]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequest request)
        {
            var validator = new ContactRequestValidator();
            var validationResult = await ValidationHelper.ValidateRequest(request, validator);

            if (validationResult.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _contactService.CreateContact(request);

            return ResponseHelper.ReturnResponse(response);
        }
    }
}
