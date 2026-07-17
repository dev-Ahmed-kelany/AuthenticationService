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
        [HttpPost(Name = "AddNewPermission")]
        public ActionResult<int> AddNewPermission(string Name, long BitValue)
        {
            return Ok(clsPermission.AddNewPermission(Name, BitValue));
        }

        [HttpPut("{ID}", Name = "UpdatePermissionByID")]
        public ActionResult<bool> UpdatePermissionByID(int ID, string Name)
        {
            return Ok(clsPermission.UpdatePermissionByID(ID, Name));
        }

        [HttpGet("Search")]
        public ActionResult<List<PermissionDTO>> SearchPermissionsByName(string SearchText)
        {
            return Ok(clsPermission.SearchPermissionsByName(SearchText));
        }

        [HttpGet("{ID}", Name = "GetPermissionByID")]
        public ActionResult<PermissionDTO> GetPermissionByID(int ID)
        {
            PermissionDTO? Permission = clsPermission.GetPermissionByID(ID);

            if (Permission == null)
                return NotFound();

            return Ok(Permission);
        }

        [HttpGet]
        public ActionResult<List<PermissionDTO>> GetAllPermissions()
        {
            return Ok(clsPermission.GetAllPermissions());
        }
    }
}
