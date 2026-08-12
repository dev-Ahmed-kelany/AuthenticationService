using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.Profile;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetProfileByUserID")]
        [Authorize(Policy = "Profile.Read")]
        [Authorize(Policy = "Ownership")]
        [ProducesResponseType(typeof(List<ProfileDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProfileDetailsDto>> GetProfileAsync(int id)
        {
            try
            {
                var result = await Profile.GetProfileAsync(id);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }
    }
}
