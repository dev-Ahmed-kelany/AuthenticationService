using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Roles")]
    public class RolesController : ControllerBase
    {
        [HttpPost(Name = "AddRole")]
        [Authorize(Policy = "Roles.Create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddAsync(SaveRoleDto role)
        {
            try
            {
                var result = await Role.AddAsync(role);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return CreatedAtRoute("GetRolesByIDAsync", new { id = result.Data }, new { id = result.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateRoleByID")]
        [Authorize(Policy = "Roles.Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateByIDAsync(int id, SaveRoleDto role)
        {
            try
            {
                var result = await Role.UpdateByIDAsync(id, role);

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
        [Authorize(Policy = "Roles.Read")]
        [ProducesResponseType(typeof(List<RoleDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<RoleDetailsDto>>> SearchByNameAsync(string searchText)
        {
            try
            {
                var result = await Role.SearchByNameAsync(searchText);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetRolesByIDAsync")]
        [Authorize(Policy = "Roles.Read")]
        [ProducesResponseType(typeof(RoleDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleDetailsDto>> GetByIDAsync(int id)
        {
            try
            {
                var result = await Role.GetByIDAsync(id);

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
        [Authorize(Policy = "Roles.Read")]
        [ProducesResponseType(typeof(List<RoleDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<RoleDetailsDto>>> GetAllAsync()
        {
            try
            {
                var result = await Role.GetAllAsync();

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
