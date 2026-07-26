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
        [HttpPost(Name = "AddRole")]
        public ActionResult<int> AddRole(string name, long permissionsMask)
        {
            return Ok(Role.AddRole(name, permissionsMask));
        }

        [HttpPut("{id}", Name = "UpdateRoleByID")]
        public ActionResult<bool> UpdateRoleByID(int id, string name, long permissionsMask)
        {
            return Ok(Role.UpdateRoleByID(id, name, permissionsMask));
        }

        [HttpGet("Search")]
        public ActionResult<List<RoleDTO>> SearchRolesByName(string searchText)
        {
            return Ok(Role.SearchRolesByName(searchText));
        }

        [HttpGet("{id}", Name = "GetRoleByID")]
        public ActionResult<RoleDTO> GetRoleByID(int id)
        {
            RoleDTO? role = Role.GetRoleByID(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpGet]
        public ActionResult<List<RoleDTO>> GetAllRoles()
        {
            return Ok(Role.GetAllRoles());
        }
    }
}
