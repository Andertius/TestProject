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
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("accounts/create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var validator = new AccountRequestValidator();
            var validationResult = await ValidationHelper.ValidateRequest(request, validator);

            if (validationResult.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _accountService.CreateAccount(request);

            return ResponseHelper.ReturnResponse(response);
        }
    }
}
