using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.AuditLogs;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthenticationService.API.Controllers
{
    [Route("api/AuditLogs")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetAuditLogByID")]
        [ProducesResponseType(typeof(AuditLogDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuditLogDetailsDto> GetAuditLogByID(int id)
        {
            try
            {
                var result = AuditLog.Find(id);

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
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<AuditLogDetailsDto>> GetAllAuditLogs()
        {
            try
            {
                var result = AuditLog.GetAll();

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occured.", Details = ex.Message
                });
            }
        }

        [HttpGet("User/{userId}")]
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<AuditLogDetailsDto>> GetAuditLogsByUserID(int userId)
        {
            try
            {
                var result = AuditLog.GetByUserID(userId);

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
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<AuditLogDetailsDto>> SearchAuditLogs(string searchText)
        {
            try
            {
                var result = AuditLog.Search(searchText);

                if (!result.IsSuccess)
                    return StatusCode(result.Error.StatusCode, new { Code = result.Error.Code, Message = result.Error.Description });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occured.", Details = ex.Message });
            }
        }

        [HttpGet("Filter")]
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<AuditLogDetailsDto>> FilterAuditLogs(int? entityId, int? operationTypeId)
        {
            try 
            {
                var result = AuditLog.Filter(entityId, operationTypeId);

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