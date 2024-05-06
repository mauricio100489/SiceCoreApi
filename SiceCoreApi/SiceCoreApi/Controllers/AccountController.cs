using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiceCoreApi.Models.Responses;
using SiceCoreApi.Models.Security;
using SiceCoreApi.Services.Security;
using System.Net;

namespace SiceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAccount(UserAccountDTO userAccountDTO)
        {
            try
            {
                var responseCreateAccount = await accountService.CreateAccount(userAccountDTO);

                return Ok(responseCreateAccount);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new Error
                {
                    Code = HttpStatusCode.InternalServerError.ToString(),
                    Target = "Internal",
                    Message = nameof(HttpStatusCode.InternalServerError),
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(UserAccountDTO userAccountDTO)
        {
            try
            {
                var responseCreateAccount = await accountService.CreateAccount(userAccountDTO);

                return Ok(responseCreateAccount);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new Error
                {
                    Code = HttpStatusCode.InternalServerError.ToString(),
                    Target = "Internal",
                    Message = nameof(HttpStatusCode.InternalServerError),
                });
            }
        }
    }
}
