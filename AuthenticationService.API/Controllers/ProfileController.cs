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
        [HttpGet("{ID}", Name = "GetProfileByUserID")]
        public ActionResult<ProfileDTO> GetProfile(int ID)
        {
            ProfileDTO? Profile = clsProfile.GetProfile(ID);

            if (Profile == null) return NotFound();

            return Ok(Profile);
        }
    }
}
