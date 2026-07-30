using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.Profile;

namespace AuthenticationService.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetProfileByUserID")]
        [ProducesResponseType(typeof(List<ProfileDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProfileDetailsDto> GetProfile(int id)
        {
            try
            {
                var result = Profile.GetProfile(id);

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
