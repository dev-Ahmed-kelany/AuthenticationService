using AuthenticationService.Business;
using AuthenticationService.Repository;
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

        [HttpGet("Search")]
        public ActionResult<List<RoleDTO>> SearchRolesByName(string SearchText)
        {
            return Ok(clsRole.SearchRolesByName(SearchText));
        }

        [HttpGet("{ID}", Name = "GetRoleByID")]
        public ActionResult<RoleDTO> GetRoleByID(int ID)
        {
            RoleDTO? Role = clsRole.GetRoleByID(ID);

            if (Role == null)
                return NotFound();

            return Ok(Role);
        }
    }
}
