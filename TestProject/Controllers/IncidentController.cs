using System.Net;

using Microsoft.AspNetCore.Mvc;

using TestProject.Application.Services;
using TestProject.Application.Validators;
using TestProject.Domain.Requests;
using TestProject.Helpers;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IncidentService _incidentService;

        public IncidentController(IncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost("incidents/create")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequest request)
        {
            var validator = new IncidentRequestValidator();
            var validationResult = await ValidationHelper.ValidateRequest(request, validator);

            if (validationResult.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _incidentService.CreateIncident(request);

            return ResponseHelper.ReturnResponse(response);
        }
    }
}
