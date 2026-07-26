using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.Permissions;

namespace AuthenticationService.API.Controllers
{
    [Route("api/Permissions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        [HttpPost(Name = "AddPermission")]
        public ActionResult<int> AddPermission(CreatePermissionDto permission)
        {
            return Ok(Permission.AddPermission(permission));
        }

        [HttpPut("{id}", Name = "UpdatePermissionByID")]
        public ActionResult<bool> UpdatePermissionByID(int id, UpdatePermissionDto permission)
        {
            return Ok(Permission.UpdatePermissionByID(id, permission));
        }

        [HttpGet("Search")]
        public ActionResult<List<PermissionDetailsDto>> SearchPermissionsByName(string searchText)
        {
            return Ok(Permission.SearchPermissionsByName(searchText));
        }

        [HttpGet("{id}", Name = "GetPermissionByID")]
        public ActionResult<PermissionDetailsDto> GetPermissionByID(int id)
        {
            PermissionDetailsDto? permission = Permission.GetPermissionByID(id);

            if (permission == null)
                return NotFound();

            return Ok(permission);
        }

        [HttpGet]
        public ActionResult<List<PermissionDetailsDto>> GetAllPermissions()
        {
            return Ok(Permission.GetAllPermissions());
        }
    }
}
