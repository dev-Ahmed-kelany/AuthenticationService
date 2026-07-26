using AuthenticationService.Business;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetProfileByUserID")]
        public ActionResult<ProfileDTO> GetProfile(int id)
        {
            ProfileDTO? profile = Profile.GetProfile(id);

            if (profile == null) return NotFound();

            return Ok(profile);
        }
    }
}
