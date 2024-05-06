using Microsoft.AspNetCore.Mvc;
using System.Net;
using SiceCoreApi.Models.Security;
using SiceCoreApi.Services.Security;
using SiceCoreApi.Models.Responses;

namespace SiceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var responseLogin = await loginService.LoginAccount(loginDTO);

                return responseLogin.Succeeded ? Ok(responseLogin) : BadRequest(responseLogin);
            }
            catch (Exception ex)
            {
                return BadRequest(new Error
                {
                    Code = HttpStatusCode.InternalServerError.ToString(),
                    Target = "Internal Error",
                    Message = ex.Message,
                });
            }
        }
    }
}
