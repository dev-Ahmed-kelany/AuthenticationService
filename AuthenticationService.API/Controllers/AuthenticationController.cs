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
        public ActionResult Login(string username,  string password)
        {
            enAuthenticationResult loginResult = Authentication.Login(username, password);

            switch (loginResult)
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
        public ActionResult VerifyCredentials(string username, string password)
        {
            enAuthenticationResult verifyCredentialsResult = Authentication.VerifyCredentials(username, password);

            switch (verifyCredentialsResult)
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
        public ActionResult ChangePassword(string username, string currentPassword, string newPassword)
        {
            enAuthenticationResult changePasswordResult = Authentication.ChangePassword(username, currentPassword, newPassword);

            switch (changePasswordResult)
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
