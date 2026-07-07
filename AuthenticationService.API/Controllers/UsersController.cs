using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuthenticationService.API.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        [HttpPost(Name = "AddNewUser")]
        public ActionResult<int> AddNewUser(string Name, string Username, string Email,
            string PasswordHash, int RoleID, int StatusID)
        {
            return Ok(clsUser.AddNewUser(Name, Username, Email, PasswordHash, RoleID, StatusID));
        }

        [HttpPut("{ID}", Name = "UpdateUserByID")]
        public ActionResult<bool> UpdateUserByID(int ID, string Name, string Username, string Email,
            int RoleID, int StatusID)
        {
            return Ok(clsUser.UpdateUserByID(ID, Name, Username, Email, RoleID, StatusID));
        }

        [HttpDelete("{ID}", Name = "DeleteUserByID")]
        public ActionResult<bool> DeleteUserByID(int ID)
        {
            return Ok(clsUser.DeleteUserByID(ID));
        }

    }
}
