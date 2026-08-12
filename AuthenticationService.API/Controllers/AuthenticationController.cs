using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using AuthenticationService.Dtos.Authentication;

namespace AuthenticationService.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost("login")]
        [EnableRateLimiting("AuthenticationServiceLimiter")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> LoginAsync(AuthenticationRequestDto request)
        {
            try
            {
                var result = await Authentication.LoginAsync(request);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpPost("refresh")]
        [EnableRateLimiting("AuthenticationServiceLimiter")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> RefreshAsync(RefreshTokenRequestDto request)
        {
            try
            {
                var result = await Authentication.RefreshAsync(request);

                if (!result.IsSuccess)
                {
                    return StatusCode(
                        result.Error.StatusCode,
                        new
                        {
                            Code = result.Error.Code,
                            Message = result.Error.Description
                        });
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = "An unexpected error occured.",
                        Details = ex.Message
                    });
            }
        }

        [HttpPost("verify-credentials")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> VerifyCredentialsAsync(AuthenticationRequestDto request)
        {
            try
            {
                var result = await Authentication.VerifyCredentialsAsync(request);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordDto request)
        {
            try
            {
                var result = await Authentication.ChangePasswordAsync(request);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogoutAsync(RefreshTokenRequestDto request)
        {
            try
            {
                var result = await Authentication.LogoutAsync(request);

                if (!result.IsSuccess)
                {
                    return StatusCode(
                        result.Error.StatusCode,
                        new
                        {
                            Code = result.Error.Code,
                            Message = result.Error.Description
                        });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = "An unexpected error occured.",
                        Details = ex.Message
                    });
            }
        }
    }
}
