using AuthenticationService.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpPost(Name = "AddNewRole")]
        public ActionResult<int> AddNewRole(string Name, long PermissionsMask)
        {
            return Ok(clsRole.AddNewRole(Name, PermissionsMask));
        }

        [HttpPut("{ID}", Name = "UpdateRoleByID")]
        public ActionResult<bool> UpdateRoleByID(int ID, string Name, long PermissionsMask)
        {
            return Ok(clsRole.UpdateRoleByID(ID, Name, PermissionsMask));
        }
    }
}
