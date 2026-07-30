using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.Users;

namespace AuthenticationService.API.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> AddUser(CreateUserDto user)
        {
            try
            {
                var result = Business.User.AddUser(user);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return CreatedAtAction("GetUserByID", result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateUserByID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUserByID(int id, UpdateUserDto user)
        {
            try
            {
                var result = Business.User.UpdateUserByID(id, user);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteUserByID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteUserByID(int id)
        {
            try
            {
                var result = Business.User.DeleteUserByID(id);

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
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UserDetailsDto>> SearchUsers(string searchText)
        {
            try
            {
                var result = Business.User.SearchUsers(searchText);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("Filter/Role/{roleId}")]
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UserDetailsDto>> FilterUsersByRoleID(int roleId)
        {
            try
            {
                var result = Business.User.FilterUsersByRoleID(roleId);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("Filter/Status/{statusId}")]
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UserDetailsDto>> FilterUsersByStatusID(int statusId)
        {
            try
            {
                var result = Business.User.FilterUsersByStatusID(statusId);

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
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UserDetailsDto>> GetAllUsers()
        {
            try
            {
                var result = Business.User.GetAllUsers();

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetUserByID")]
        [ProducesResponseType(typeof(List<UserDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDetailsDto> GetUserByID(int id)
        {
            try
            {
                var result = Business.User.GetUserByID(id);

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
