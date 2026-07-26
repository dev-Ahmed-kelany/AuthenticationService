using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using Microsoft.AspNetCore.Http.HttpResults;
using AuthenticationService.Repository;

namespace AuthenticationService.API.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        [HttpPost(Name = "AddUser")]
        public ActionResult<int> AddUser(string name, string username, string email,
            string password, int roleId, int statusId)
        {
            return base.Ok(Business.User.AddUser(name, username, email, password, roleId, statusId));
        }

        [HttpPut("{id}", Name = "UpdateUserByID")]
        public ActionResult<bool> UpdateUserByID(int id, string name, string username, string email,
            int roleId, int statusId)
        {
            return base.Ok(Business.User.UpdateUserByID(id, name, username, email, roleId, statusId));
        }

        [HttpDelete("{id}", Name = "DeleteUserByID")]
        public ActionResult<bool> DeleteUserByID(int id)
        {
            return base.Ok(Business.User.DeleteUserByID(id));
        }

        [HttpGet("Search")]
        public ActionResult<List<UserDTO>> SearchUsers(string searchText)
        {
            return base.Ok(Business.User.SearchUsers(searchText));
        }

        [HttpGet("Filter/Role/{roleId}")]
        public ActionResult<List<UserDTO>> FilterUsersByRoleID(int roleId)
        {
            return base.Ok(Business.User.FilterUsersByRoleID(roleId));
        }

        [HttpGet("Filter/Status/{statusId}")]
        public ActionResult<List<UserDTO>> FilterUsersByStatusID(int statusId)
        {
            return base.Ok(Business.User.FilterUsersByStatusID(statusId));
        }

        [HttpGet]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            return base.Ok(Business.User.GetAllUsers());
        }

        [HttpGet("{id}", Name = "GetUserByID")]
        public ActionResult<UserDTO> GetUserByID(int id)
        {
            UserDTO? user = AuthenticationService.Business.User.GetUserByID(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
