using AuthenticationService.Repository;
using AuthenticationService.Dtos.AuditLogs;

namespace AuthenticationService.Business
{
    public static class AuditLogErrors
    {
        public static readonly Error IsNull = new Error("AuditLog.IsNull", "AuditLog is null", HttpStatus.BadRequest);
        public static readonly Error NotCreated = new Error("AuditLog.NotCreated", "AuditLog not created successfully.", HttpStatus.InternalServerError);
        public static readonly Error InvalidID = new Error("AuditLog.InvalidID", "ID must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidUserID = new Error("AuditLog.InvalidUserID", "UserID must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidEntityID = new Error("AuditLog.InvalidEntityID", "EntityID must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error InvalidOperationTypeID = new Error("AuditLog.InvalidOperationTypeID", "OperationTypeID must be greater than zero.", HttpStatus.BadRequest);
        public static readonly Error NotFound = new Error("AuditLog.NotFound", "AuditLog is not found.", HttpStatus.NotFound);

    }

    public static class AuditLog
    {
        public static Result<int> Add(CreateAuditLogDto auditLog)
        {
            if (auditLog == null)
                return Result<int>.Failure(AuditLogErrors.IsNull);

            int newAuditLogId = AuditLogRepository.Add(auditLog);

            if (newAuditLogId == -1)
                return Result<int>.Failure(AuditLogErrors.NotCreated);

            return Result<int>.Success(newAuditLogId);
        }

        public static Result<AuditLogDetailsDto> Find(int id)
        {
            if (id <= 0)
                return Result<AuditLogDetailsDto>.Failure(AuditLogErrors.InvalidID);

            var auditLog = AuditLogRepository.GetAuditLogByID(id);

            if (auditLog == null)
                return Result<AuditLogDetailsDto>.Failure(AuditLogErrors.NotFound);

            return Result<AuditLogDetailsDto>.Success(auditLog);
        }

        public static Result<List<AuditLogDetailsDto>> GetAll()
        {
            List<AuditLogDetailsDto> auditLogList = AuditLogRepository.GetAllAuditLogs();

            return Result<List<AuditLogDetailsDto>>.Success(auditLogList);
        }

        public static Result<List<AuditLogDetailsDto>> GetByUserID(int userId)
        {
            if (userId <= 0)
                return Result<List<AuditLogDetailsDto>>.Failure(AuditLogErrors.InvalidUserID);

            List<AuditLogDetailsDto> auditLogList = AuditLogRepository.GetAuditLogsByUserID(userId);

            return Result<List<AuditLogDetailsDto>>.Success(auditLogList);
        }

        public static Result<List<AuditLogDetailsDto>> Search(string searchText)
        {
            List<AuditLogDetailsDto> auditLogList = AuditLogRepository.SearchAuditLogs(searchText);

            return Result<List<AuditLogDetailsDto>>.Success(auditLogList);
        }

        public static Result<List<AuditLogDetailsDto>> Filter(int? entityId, int? operationTypeId)
        {
            if (entityId <= 0)
                return Result<List<AuditLogDetailsDto>>.Failure(AuditLogErrors.InvalidEntityID);

            if (operationTypeId <= 0)
                return Result<List<AuditLogDetailsDto>>.Failure(AuditLogErrors.InvalidOperationTypeID);

            List<AuditLogDetailsDto> auditLogList = AuditLogRepository.FilterAuditLogs(entityId, operationTypeId);

            return Result<List<AuditLogDetailsDto>>.Success(auditLogList);
        }
    }
}