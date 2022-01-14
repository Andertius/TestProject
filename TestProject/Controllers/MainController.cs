using Microsoft.AspNetCore.Mvc;

using TestProject.Application;
using TestProject.Domain.Models;
using TestProject.Requests;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly AccountService _accountService;
        private readonly IncidentService _incidentService;

        public MainController(IncidentService incidentService, ContactService contactService, AccountService accountService)
        {
            _accountService = accountService;
            _contactService = contactService;
            _incidentService = incidentService;
        }

        [HttpPost("contacts/create")]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequest request)
        {
            await _contactService.CreateContact(new Contact
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            });

            return NoContent();
        }

        [HttpPost("accounts/create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var account = new Account { Name = request.Name };
            if (await _accountService.CreateAccount(account, request.Email))
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("incidents/create")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            var contact = new Contact { Email = request.Email, FirstName = request.FirstName, LastName = request.LastName };
            var incident = new Incident { Description = request.Description, Name = Guid.NewGuid().ToString() };
            await _incidentService.CreateIncident(incident, request.AccountName, contact);

            return NoContent();
        }
    }
}
