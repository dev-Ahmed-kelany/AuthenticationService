using AuthenticationService.Business;
using AuthenticationService.Dtos.AuditLogs;
using AuthenticationService.Dtos.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Security;

namespace AuthenticationService.API.Controllers
{
    [Route("api/Permissions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        [HttpPost(Name = "AddPermission")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> AddPermission(CreatePermissionDto permission)
        {
            try
            {
                var result = Permission.AddPermission(permission);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return CreatedAtAction("GetPermissionByID", result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdatePermissionByID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePermissionByID(int id, UpdatePermissionDto permission)
        {
            try
            {
                var result = Permission.UpdatePermissionByID(id, permission);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<PermissionDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PermissionDetailsDto>> SearchPermissionsByName(string searchText)
        {
            try
            {
                var result = Permission.SearchPermissionsByName(searchText);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetPermissionByID")]
        [ProducesResponseType(typeof(List<PermissionDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PermissionDetailsDto> GetPermissionByID(int id)
        {
            try
            {
                var result = Permission.GetPermissionByID(id);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PermissionDetailsDto>> GetAllPermissions()
        {
            try
            {
                var result = Permission.GetAllPermissions();

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }
    }
}
