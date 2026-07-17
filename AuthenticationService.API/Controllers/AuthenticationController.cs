using AuthenticationService.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost("login")]
        public ActionResult Login(string Username,  string Password)
        {
            enLoginResult LoginResult = clsAuthentication.Login(Username, Password);

            switch (LoginResult)
            {
                case enLoginResult.Success:
                    return Ok();
                case enLoginResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case enLoginResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

    }
}
