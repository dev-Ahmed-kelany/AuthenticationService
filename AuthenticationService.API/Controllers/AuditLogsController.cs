using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.AuditLogs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/AuditLogs")]
    public class AuditLogsController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetAuditLogByID")]
        [Authorize(Policy = "AuditLogs.Read")]
        [ProducesResponseType(typeof(AuditLogDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuditLogDetailsDto>> GetByIDAsync(int id)
        {
            try
            {
                var result = await AuditLog.GetByIDAsync(id);

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
        [Authorize(Policy = "AuditLogs.Read")]
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AuditLogDetailsDto>>> GetAllAsync()
        {
            try
            {
                var result = await AuditLog.GetAllAsync();

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
        [Authorize(Policy = "AuditLogs.Read")]
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AuditLogDetailsDto>>> GetByUserIDAsync(int userId)
        {
            try
            {
                var result = await AuditLog.GetByUserIDAsync(userId);

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
        [Authorize(Policy = "AuditLogs.Read")]
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AuditLogDetailsDto>>> SearchAsync(string searchText)
        {
            try
            {
                var result = await AuditLog.SearchAsync(searchText);

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
        [Authorize(Policy = "AuditLogs.Read")]
        [ProducesResponseType(typeof(List<AuditLogDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AuditLogDetailsDto>>> FilterAsync(int? entityId, int? operationTypeId)
        {
            try 
            {
                var result = await AuditLog.FilterAsync(entityId, operationTypeId);

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