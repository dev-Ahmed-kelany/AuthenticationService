using AuthenticationService.Business;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/LoginHistory")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        [HttpPost(Name = "AddNewLoginHistory")]
        public ActionResult<int> AddNewLoginHistory(LoginHistoryDTO LoginHistory)
        {
            return Ok(clsLoginHistory.AddNewLoginHistory(LoginHistory));
        }

        [HttpGet("{ID}", Name = "GetLoginHistoryByID")]
        public ActionResult<LoginHistoryDTO> GetLoginHistoryByID(int ID)
        {
            LoginHistoryDTO? LoginHistory = clsLoginHistory.Find(ID);

            if (LoginHistory == null)
                return NotFound();

            return Ok(LoginHistory);
        }

        [HttpGet]
        public ActionResult<List<LoginHistoryDTO>> GetAllLoginHistory()
        {
            return Ok(clsLoginHistory.GetAll());
        }

        [HttpGet("User/{UserID}")]
        public ActionResult<List<LoginHistoryDTO>> GetLoginHistoryByUserID(int UserID)
        {
            return Ok(clsLoginHistory.GetByUserID(UserID));
        }

        [HttpGet("Search")]
        public ActionResult<List<LoginHistoryDTO>> SearchLoginHistory(string SearchText)
        {
            return Ok(clsLoginHistory.Search(SearchText));
        }

        [HttpGet("Status/{Status}")]
        public ActionResult<List<LoginHistoryDTO>> FilterLoginHistoryByStatus(byte Status)
        {
            return Ok(clsLoginHistory.FilterByStatus(Status));
        }
    }
}