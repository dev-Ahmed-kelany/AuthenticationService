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
        public ActionResult<ProfileDetailsDto> GetProfile(int id)
        {
            ProfileDetailsDto? profile = Profile.GetProfile(id);

            if (profile == null) return NotFound();

            return Ok(profile);
        }
    }
}
