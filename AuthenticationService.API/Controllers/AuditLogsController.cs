using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Business;
using AuthenticationService.Dtos.AuditLogs;

namespace AuthenticationService.API.Controllers
{
    [Route("api/AuditLogs")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        [HttpPost(Name = "AddAuditLog")]
        public ActionResult<int> AddAuditLog(CreateAuditLogDto auditLog)
        {
            return base.Ok(Business.AuditLog.AddAuditLog(auditLog));
        }

        [HttpGet("{id}", Name = "GetAuditLogByID")]
        public ActionResult<AuditLogDetailsDto> GetAuditLogByID(int id)
        {
            AuditLogDetailsDto? auditLog = AuditLog.Find(id);

            if (auditLog == null)
                return NotFound();

            return Ok(auditLog);
        }

        [HttpGet]
        public ActionResult<List<AuditLogDetailsDto>> GetAllAuditLogs()
        {
            return Ok(AuditLog.GetAll());
        }

        [HttpGet("User/{userId}")]
        public ActionResult<List<AuditLogDetailsDto>> GetAuditLogsByUserID(int userId)
        {
            return Ok(AuditLog.GetByUserID(userId));
        }

        [HttpGet("Search")]
        public ActionResult<List<AuditLogDetailsDto>> SearchAuditLogs(string searchText)
        {
            return Ok(AuditLog.Search(searchText));
        }

        [HttpGet("Filter")]
        public ActionResult<List<AuditLogDetailsDto>> FilterAuditLogs(int? entityId, int? operationTypeId)
        {
            return Ok(AuditLog.Filter(entityId, operationTypeId));
        }
    }
}