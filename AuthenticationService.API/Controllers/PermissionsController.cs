using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using Microsoft.AspNetCore.Authorization;
using AuthenticationService.Dtos.Permissions;

namespace AuthenticationService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Permissions")]
    public class PermissionsController : ControllerBase
    {
        [HttpPost(Name = "AddPermission")]
        [Authorize(Policy = "Permissions.Create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddAsync(CreatePermissionDto permission)
        {
            try
            {
                var result = await Permission.AddAsync(permission);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return CreatedAtRoute("GetByIDAsync", new { id = result.Data }, new { id = result.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdatePermissionByID")]
        [Authorize(Policy = "Permissions.Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateByIDAsync(int id, UpdatePermissionDto permission)
        {
            try
            {
                var result = await Permission.UpdateByIDAsync(id, permission);

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
        [Authorize(Policy = "Permissions.Read")]
        [ProducesResponseType(typeof(List<PermissionDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PermissionDetailsDto>>> SearchByNameAsync(string searchText)
        {
            try
            {
                var result = await Permission.SearchByNameAsync(searchText);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetPermissionsByIDAsync")]
        [Authorize(Policy = "Permissions.Read")]
        [ProducesResponseType(typeof(PermissionDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PermissionDetailsDto>> GetByIDAsync(int id)
        {
            try
            {
                var result = await Permission.GetByIDAsync(id);

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
        [Authorize(Policy = "Permissions.Read")]
        [ProducesResponseType(typeof(List<PermissionDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PermissionDetailsDto>>> GetAllAsync()
        {
            try
            {
                var result = await Permission.GetAllAsync();

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
