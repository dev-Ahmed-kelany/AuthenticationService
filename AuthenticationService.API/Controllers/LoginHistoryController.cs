using AuthenticationService.Business;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/LoginHistory")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        [HttpPost(Name = "AddLoginHistory")]
        public ActionResult<int> AddLoginHistory(LoginHistoryDTO loginHistory)
        {
            return base.Ok(Business.LoginHistory.AddLoginHistory(loginHistory));
        }

        [HttpGet("{ID}", Name = "GetLoginHistoryByID")]
        public ActionResult<LoginHistoryDTO> GetLoginHistoryByID(int id)
        {
            LoginHistoryDTO? loginHistory = LoginHistory.Find(id);

            if (loginHistory == null)
                return NotFound();

            return Ok(loginHistory);
        }

        [HttpGet]
        public ActionResult<List<LoginHistoryDTO>> GetAllLoginHistory()
        {
            return Ok(LoginHistory.GetAll());
        }

        [HttpGet("User/{userId}")]
        public ActionResult<List<LoginHistoryDTO>> GetLoginHistoryByUserID(int userId)
        {
            return Ok(LoginHistory.GetByUserID(userId));
        }

        [HttpGet("Search")]
        public ActionResult<List<LoginHistoryDTO>> SearchLoginHistory(string searchText)
        {
            return Ok(LoginHistory.Search(searchText));
        }

        [HttpGet("Status/{Status}")]
        public ActionResult<List<LoginHistoryDTO>> FilterLoginHistoryByStatus(byte status)
        {
            return Ok(LoginHistory.FilterByStatus(status));
        }
    }
}