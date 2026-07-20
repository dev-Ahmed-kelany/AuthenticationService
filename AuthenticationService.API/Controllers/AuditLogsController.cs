using AuthenticationService.Business;
using AuthenticationService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.API.Controllers
{
    [Route("api/AuditLogs")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        [HttpPost(Name = "AddNewAuditLog")]
        public ActionResult<int> AddNewAuditLog(AuditLogDTO AuditLog)
        {
            return Ok(clsAuditLog.AddNewAuditLog(AuditLog));
        }

        [HttpGet("{ID}", Name = "GetAuditLogByID")]
        public ActionResult<AuditLogDTO> GetAuditLogByID(int ID)
        {
            AuditLogDTO? AuditLog = clsAuditLog.Find(ID);

            if (AuditLog == null)
                return NotFound();

            return Ok(AuditLog);
        }

        [HttpGet]
        public ActionResult<List<AuditLogDTO>> GetAllAuditLogs()
        {
            return Ok(clsAuditLog.GetAll());
        }

        [HttpGet("User/{UserID}")]
        public ActionResult<List<AuditLogDTO>> GetAuditLogsByUserID(int UserID)
        {
            return Ok(clsAuditLog.GetByUserID(UserID));
        }

        [HttpGet("Search")]
        public ActionResult<List<AuditLogDTO>> SearchAuditLogs(string SearchText)
        {
            return Ok(clsAuditLog.Search(SearchText));
        }

        [HttpGet("Filter")]
        public ActionResult<List<AuditLogDTO>> FilterAuditLogs(int? EntityID, int? OperationTypeID)
        {
            return Ok(clsAuditLog.Filter(EntityID, OperationTypeID));
        }
    }
}