using AuthenticationService.Business;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/AuditLogs")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        [HttpPost(Name = "AddAuditLog")]
        public ActionResult<int> AddAuditLog(AuditLogDTO auditLog)
        {
            return base.Ok(Business.AuditLog.AddAuditLog(auditLog));
        }

        [HttpGet("{id}", Name = "GetAuditLogByID")]
        public ActionResult<AuditLogDTO> GetAuditLogByID(int id)
        {
            AuditLogDTO? auditLog = AuditLog.Find(id);

            if (auditLog == null)
                return NotFound();

            return Ok(auditLog);
        }

        [HttpGet]
        public ActionResult<List<AuditLogDTO>> GetAllAuditLogs()
        {
            return Ok(AuditLog.GetAll());
        }

        [HttpGet("User/{userId}")]
        public ActionResult<List<AuditLogDTO>> GetAuditLogsByUserID(int userId)
        {
            return Ok(AuditLog.GetByUserID(userId));
        }

        [HttpGet("Search")]
        public ActionResult<List<AuditLogDTO>> SearchAuditLogs(string searchText)
        {
            return Ok(AuditLog.Search(searchText));
        }

        [HttpGet("Filter")]
        public ActionResult<List<AuditLogDTO>> FilterAuditLogs(int? entityId, int? operationTypeId)
        {
            return Ok(AuditLog.Filter(entityId, operationTypeId));
        }
    }
}