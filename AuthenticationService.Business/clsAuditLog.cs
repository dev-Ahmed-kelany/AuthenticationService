using AuthenticationService.Repository;

namespace AuthenticationService.Business
{
    public static class clsAuditLog
    {
        public static int AddNewAuditLog(AuditLogDTO AuditLog)
        {
            return clsAuditLogRepository.AddNewAuditLog(AuditLog);
        }

        public static AuditLogDTO? Find(int ID)
        {
            return clsAuditLogRepository.GetAuditLogByID(ID);
        }

        public static List<AuditLogDTO> GetAll()
        {
            return clsAuditLogRepository.GetAllAuditLogs();
        }

        public static List<AuditLogDTO> GetByUserID(int UserID)
        {
            return clsAuditLogRepository.GetAuditLogsByUserID(UserID);
        }

        public static List<AuditLogDTO> Search(string SearchText)
        {
            return clsAuditLogRepository.SearchAuditLogs(SearchText);
        }

        public static List<AuditLogDTO> Filter(int? EntityID, int? OperationTypeID)
        {
            return clsAuditLogRepository.FilterAuditLogs(EntityID, OperationTypeID);
        }
    }
}