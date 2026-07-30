using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.AuditLogs;

namespace AuthenticationService.Repository
{
    public class AuditLogRepository
    {
        private static AuditLogDetailsDto _MapAuditLogDTO(SqlDataReader reader)
        {
            return new AuditLogDetailsDto
            {
                ID = (int)reader["ID"],
                UserID = (int)reader["UserID"],
                Username = (string)reader["Username"],
                Name = (string)reader["Name"],
                EntityID = (int)reader["EntityID"],
                EntityName = (string)reader["EntityName"],
                OperationTypeID = (int)reader["OperationTypeID"],
                OperationTypeName = (string)reader["OperationTypeName"],
                DateTime = (DateTime)reader["DateTime"]
            };
        }

        public static async Task<int> AddAsync(CreateAuditLogDto auditLog)
        {
            int newAuditLogID = -1;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewAuditLog", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", auditLog.UserID);
                    command.Parameters.AddWithValue("@EntityID", auditLog.EntityID);
                    command.Parameters.AddWithValue("@OperationTypeID", auditLog.OperationTypeID);

                    SqlParameter outputParameter = new SqlParameter("@NewAuditLogID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputParameter);

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    newAuditLogID = (int)outputParameter.Value;
                }
            }

            return newAuditLogID;
        }

        public static async Task<AuditLogDetailsDto?> GetByIDAsync(int id)
        {
            AuditLogDetailsDto? auditLog = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuditLogByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            auditLog = _MapAuditLogDTO(reader);
                    }
                }
            }

            return auditLog;
        }

        public static async Task<List<AuditLogDetailsDto>> GetAllAsync()
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllAuditLogs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

        public static async Task<List<AuditLogDetailsDto>> GetByUserIDAsync(int userId)
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuditLogsByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

        public static async Task<List<AuditLogDetailsDto>> SearchAsync(string searchText)
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchAuditLogs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

        public static async Task<List<AuditLogDetailsDto>> FilterAsync(int? entityId, int? operationTypeId)
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FilterAuditLogs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EntityID",
                        entityId.HasValue ? entityId.Value : DBNull.Value);

                    command.Parameters.AddWithValue("@OperationTypeID",
                        operationTypeId.HasValue ? operationTypeId.Value : DBNull.Value);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

    }
}
