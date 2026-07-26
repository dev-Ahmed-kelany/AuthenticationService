using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public static class AuditLog
    {
        public static int AddAuditLog(AuditLogDTO auditLog)
        {
            return AuditLogRepository.AddAuditLog(auditLog);
        }

        public static AuditLogDTO? Find(int id)
        {
            return AuditLogRepository.GetAuditLogByID(id);
        }

        public static List<AuditLogDTO> GetAll()
        {
            return AuditLogRepository.GetAllAuditLogs();
        }

        public static List<AuditLogDTO> GetByUserID(int userId)
        {
            return AuditLogRepository.GetAuditLogsByUserID(userId);
        }

        public static List<AuditLogDTO> Search(string searchText)
        {
            return AuditLogRepository.SearchAuditLogs(searchText);
        }

        public static List<AuditLogDTO> Filter(int? entityId, int? operationTypeId)
        {
            return AuditLogRepository.FilterAuditLogs(entityId, operationTypeId);
        }
    }
}