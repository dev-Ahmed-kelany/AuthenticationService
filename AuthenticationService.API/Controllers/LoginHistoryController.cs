using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.LoginHistory;

namespace AuthenticationService.API.Controllers
{
    [Route("api/LoginHistory")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        [HttpPost(Name = "AddLoginHistory")]
        public ActionResult<int> AddLoginHistory(CreateLoginHistoryDto loginHistory)
        {
            return base.Ok(Business.LoginHistory.AddLoginHistory(loginHistory));
        }

        [HttpGet("{id}", Name = "GetLoginHistoryByID")]
        public ActionResult<LoginHistoryDetailsDto> GetLoginHistoryByID(int id)
        {
            LoginHistoryDetailsDto? loginHistory = LoginHistory.Find(id);

            if (loginHistory == null)
                return NotFound();

            return Ok(loginHistory);
        }

        [HttpGet]
        public ActionResult<List<LoginHistoryDetailsDto>> GetAllLoginHistory()
        {
            return Ok(LoginHistory.GetAll());
        }

        [HttpGet("User/{userId}")]
        public ActionResult<List<LoginHistoryDetailsDto>> GetLoginHistoryByUserID(int userId)
        {
            return Ok(LoginHistory.GetByUserID(userId));
        }

        [HttpGet("Search")]
        public ActionResult<List<LoginHistoryDetailsDto>> SearchLoginHistory(string searchText)
        {
            return Ok(LoginHistory.Search(searchText));
        }

        [HttpGet("Status/{status}")]
        public ActionResult<List<LoginHistoryDetailsDto>> FilterLoginHistoryByStatus(byte status)
        {
            return Ok(LoginHistory.FilterByStatus(status));
        }
    }
}