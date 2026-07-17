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
            enAuthenticationResult LoginResult = clsAuthentication.Login(Username, Password);

            switch (LoginResult)
            {
                case enAuthenticationResult.Success:
                    return Ok();
                case enAuthenticationResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case enAuthenticationResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

        [HttpPost("verify-credentials")]
        public ActionResult VerifyCredentials(string Username, string Password)
        {
            enAuthenticationResult VerifyCredentialsResult = clsAuthentication.VerifyCredentials(Username, Password);

            switch (VerifyCredentialsResult)
            {
                case enAuthenticationResult.Success:
                    return Ok();
                case enAuthenticationResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case enAuthenticationResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

        [HttpPost("change-password")]
        public ActionResult ChangePassword(string Username, string CurrentPassword, string NewPassword)
        {
            enAuthenticationResult ChangePasswordResult = clsAuthentication.ChangePassword(Username, CurrentPassword, NewPassword);

            switch (ChangePasswordResult)
            {
                case enAuthenticationResult.Success:
                    return Ok();
                case enAuthenticationResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case enAuthenticationResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

    }
}
