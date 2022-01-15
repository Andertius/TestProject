using Microsoft.AspNetCore.Mvc;

using TestProject.Application.Services;
using TestProject.Application.Validators;
using TestProject.Domain;
using TestProject.Domain.Models;
using TestProject.Domain.Requests;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly AccountService _accountService;
        private readonly IncidentService _incidentService;

        public MainController(
            IncidentService incidentService,
            ContactService contactService,
            AccountService accountService)
        {
            _accountService = accountService;
            _contactService = contactService;
            _incidentService = incidentService;
        }

        [HttpPost("contacts/create")]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequest request)
        {
            var validator = new ContactRequestValidator();

            if (!(await validator.ValidateAsync(request)).IsValid)
            {
                return BadRequest((await validator
                    .ValidateAsync(request))
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .Aggregate((x, y) => $"{x}\n{y}"));
            }

            var contact = new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            var response =  await _contactService.CreateContact(contact);

            return response.Result switch
            {
                OperationResult.Failure => BadRequest(response.Error),
                OperationResult.NotFound => NotFound(response.Error),
                OperationResult.Success => NoContent(),
                _ => throw new NotSupportedException(),
            };
        }

        [HttpPost("accounts/create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var validator = new AccountRequestValidator();

            if (!(await validator.ValidateAsync(request)).IsValid)
            {
                return BadRequest((await validator
                    .ValidateAsync(request))
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .Aggregate((x, y) => $"{x}\n{y}"));
            }

            var response = await _accountService.CreateAccount(request.Name, request.Email);

            return response.Result switch
            {
                OperationResult.Failure => BadRequest(response.Error),
                OperationResult.NotFound => NotFound(response.Error),
                OperationResult.Success => NoContent(),
                _ => throw new NotSupportedException(),
            };
        }

        [HttpPost("incidents/create")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            var validator = new IncidentRequestValidator();

            if (!(await validator.ValidateAsync(request)).IsValid)
            {
                return BadRequest((await validator
                    .ValidateAsync(request))
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .Aggregate((x, y) => $"{x}\n{y}"));
            }

            var contact = new Contact { Email = request.Email, FirstName = request.FirstName, LastName = request.LastName };
            var incident = new Incident { Description = request.Description, Name = Guid.NewGuid().ToString() };
            var response = await _incidentService.CreateIncident(incident, request.AccountName, contact);

            return response.Result switch
            {
                OperationResult.Failure => BadRequest(response.Error),
                OperationResult.NotFound => NotFound(response.Error),
                OperationResult.Success => NoContent(),
                _ => throw new NotSupportedException(),
            };
        }
    }
}
