using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Dtos.Users;

namespace AuthenticationService.API.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        [HttpPost(Name = "AddUser")]
        public ActionResult<int> AddUser(CreateUserDto user)
        {
            return base.Ok(Business.User.AddUser(user));
        }

        [HttpPut("{id}", Name = "UpdateUserByID")]
        public ActionResult<bool> UpdateUserByID(int id, UpdateUserDto user)
        {
            return base.Ok(Business.User.UpdateUserByID(id, user));
        }

        [HttpDelete("{id}", Name = "DeleteUserByID")]
        public ActionResult<bool> DeleteUserByID(int id)
        {
            return base.Ok(Business.User.DeleteUserByID(id));
        }

        [HttpGet("Search")]
        public ActionResult<List<UserDetailsDto>> SearchUsers(string searchText)
        {
            return base.Ok(Business.User.SearchUsers(searchText));
        }

        [HttpGet("Filter/Role/{roleId}")]
        public ActionResult<List<UserDetailsDto>> FilterUsersByRoleID(int roleId)
        {
            return base.Ok(Business.User.FilterUsersByRoleID(roleId));
        }

        [HttpGet("Filter/Status/{statusId}")]
        public ActionResult<List<UserDetailsDto>> FilterUsersByStatusID(int statusId)
        {
            return base.Ok(Business.User.FilterUsersByStatusID(statusId));
        }

        [HttpGet]
        public ActionResult<List<UserDetailsDto>> GetAllUsers()
        {
            return base.Ok(Business.User.GetAllUsers());
        }

        [HttpGet("{id}", Name = "GetUserByID")]
        public ActionResult<UserDetailsDto> GetUserByID(int id)
        {
            UserDetailsDto? user = AuthenticationService.Business.User.GetUserByID(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
