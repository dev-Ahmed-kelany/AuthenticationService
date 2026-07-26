using AuthenticationService.Repository;
using AuthenticationService.Dtos.AuditLogs;

namespace AuthenticationService.Business
{
    public static class AuditLog
    {
        public static int AddAuditLog(CreateAuditLogDto auditLog)
        {
            return AuditLogRepository.AddAuditLog(auditLog);
        }

        public static AuditLogDetailsDto? Find(int id)
        {
            return AuditLogRepository.GetAuditLogByID(id);
        }

        public static List<AuditLogDetailsDto> GetAll()
        {
            return AuditLogRepository.GetAllAuditLogs();
        }

        public static List<AuditLogDetailsDto> GetByUserID(int userId)
        {
            return AuditLogRepository.GetAuditLogsByUserID(userId);
        }

        public static List<AuditLogDetailsDto> Search(string searchText)
        {
            return AuditLogRepository.SearchAuditLogs(searchText);
        }

        public static List<AuditLogDetailsDto> Filter(int? entityId, int? operationTypeId)
        {
            return AuditLogRepository.FilterAuditLogs(entityId, operationTypeId);
        }
    }
}