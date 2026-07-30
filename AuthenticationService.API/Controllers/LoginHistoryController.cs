using AuthenticationService.Business;
using AuthenticationService.Dtos.AuditLogs;
using AuthenticationService.Dtos.LoginHistory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace AuthenticationService.API.Controllers
{
    [Route("api/LoginHistory")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetLoginHistoryByID")]
        [ProducesResponseType(typeof(LoginHistoryDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LoginHistoryDetailsDto> GetLoginHistoryByID(int id)
        {
            try
            {
                var result = LoginHistory.Find(id);

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
        [ProducesResponseType(typeof(List<LoginHistoryDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<LoginHistoryDetailsDto>> GetAllLoginHistory()
        {
            try
            {
                var result = LoginHistory.GetAll();

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occured.",
                    Details = ex.Message
                });
            }
        }

        [HttpGet("User/{userId}")]
        [ProducesResponseType(typeof(List<LoginHistoryDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<LoginHistoryDetailsDto>> GetLoginHistoryByUserID(int userId)
        {
            try
            {
                var result = LoginHistory.GetByUserID(userId);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<LoginHistoryDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<LoginHistoryDetailsDto>> SearchLoginHistory(string searchText)
        {
            try
            {
                var result = LoginHistory.Search(searchText);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("Status/{status}")]
        [ProducesResponseType(typeof(List<LoginHistoryDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<LoginHistoryDetailsDto>> FilterLoginHistoryByStatus(byte status)
        {
            try
            {
                var result = LoginHistory.FilterByStatus(status);

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