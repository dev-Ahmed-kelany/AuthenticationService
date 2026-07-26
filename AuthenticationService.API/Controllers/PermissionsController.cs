using AuthenticationService.Business;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/Permissions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        [HttpPost(Name = "AddPermission")]
        public ActionResult<int> AddPermission(string name, long bitValue)
        {
            return Ok(Permission.AddPermission(name, bitValue));
        }

        [HttpPut("{id}", Name = "UpdatePermissionByID")]
        public ActionResult<bool> UpdatePermissionByID(int id, string name)
        {
            return Ok(Permission.UpdatePermissionByID(id, name));
        }

        [HttpGet("Search")]
        public ActionResult<List<PermissionDTO>> SearchPermissionsByName(string searchText)
        {
            return Ok(Permission.SearchPermissionsByName(searchText));
        }

        [HttpGet("{id}", Name = "GetPermissionByID")]
        public ActionResult<PermissionDTO> GetPermissionByID(int id)
        {
            PermissionDTO? permission = Permission.GetPermissionByID(id);

            if (permission == null)
                return NotFound();

            return Ok(permission);
        }

        [HttpGet]
        public ActionResult<List<PermissionDTO>> GetAllPermissions()
        {
            return Ok(Permission.GetAllPermissions());
        }
    }
}
