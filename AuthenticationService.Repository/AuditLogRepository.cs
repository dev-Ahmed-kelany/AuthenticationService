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

        public static int AddAuditLog(CreateAuditLogDto auditLog)
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

                    connection.Open();

                    command.ExecuteNonQuery();

                    newAuditLogID = (int)outputParameter.Value;
                }
            }

            return newAuditLogID;
        }

        public static AuditLogDetailsDto? GetAuditLogByID(int id)
        {
            AuditLogDetailsDto? auditLog = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuditLogByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            auditLog = _MapAuditLogDTO(reader);
                    }
                }
            }

            return auditLog;
        }

        public static List<AuditLogDetailsDto> GetAllAuditLogs()
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllAuditLogs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

        public static List<AuditLogDetailsDto> GetAuditLogsByUserID(int userId)
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuditLogsByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

        public static List<AuditLogDetailsDto> SearchAuditLogs(string searchText)
        {
            List<AuditLogDetailsDto> auditLogs = new List<AuditLogDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchAuditLogs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            auditLogs.Add(_MapAuditLogDTO(reader));
                        }
                    }
                }
            }

            return auditLogs;
        }

        public static List<AuditLogDetailsDto> FilterAuditLogs(int? entityId, int? operationTypeId)
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

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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
