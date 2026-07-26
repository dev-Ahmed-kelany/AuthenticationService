using AuthenticationService.Business;
using AuthenticationService.Dtos.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost("login")]
        public ActionResult Login(LoginRequestDto request)
        {
            AuthenticationResult loginResult = Authentication.Login(request);

            switch (loginResult)
            {
                case AuthenticationResult.Success:
                    return Ok();
                case AuthenticationResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case AuthenticationResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

        [HttpPost("verify-credentials")]
        public ActionResult VerifyCredentials(LoginRequestDto request)
        {
            AuthenticationResult verifyCredentialsResult = Authentication.VerifyCredentials(request);

            switch (verifyCredentialsResult)
            {
                case AuthenticationResult.Success:
                    return Ok();
                case AuthenticationResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case AuthenticationResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

        [HttpPost("change-password")]
        public ActionResult ChangePassword(ChangePasswordDto request)
        {
            AuthenticationResult changePasswordResult = Authentication.ChangePassword(request);

            switch (changePasswordResult)
            {
                case AuthenticationResult.Success:
                    return Ok();
                case AuthenticationResult.InvalidCredentials:
                    return BadRequest("Invalid Credentials.");
                case AuthenticationResult.InactiveAccount:
                    return BadRequest("Account is inactive.");
                default:
                    return BadRequest();
            }
        }

    }
}
