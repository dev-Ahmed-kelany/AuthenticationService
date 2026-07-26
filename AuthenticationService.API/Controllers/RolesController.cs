using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.Roles;

namespace AuthenticationService.API.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpPost(Name = "AddRole")]
        public ActionResult<int> AddRole(SaveRoleDto role)
        {
            return Ok(Role.AddRole(role));
        }

        [HttpPut("{id}", Name = "UpdateRoleByID")]
        public ActionResult<bool> UpdateRoleByID(int id, SaveRoleDto role)
        {
            return Ok(Role.UpdateRoleByID(id, role));
        }

        [HttpGet("Search")]
        public ActionResult<List<RoleDetailsDto>> SearchRolesByName(string searchText)
        {
            return Ok(Role.SearchRolesByName(searchText));
        }

        [HttpGet("{id}", Name = "GetRoleByID")]
        public ActionResult<RoleDetailsDto> GetRoleByID(int id)
        {
            RoleDetailsDto? role = Role.GetRoleByID(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpGet]
        public ActionResult<List<RoleDetailsDto>> GetAllRoles()
        {
            return Ok(Role.GetAllRoles());
        }
    }
}
