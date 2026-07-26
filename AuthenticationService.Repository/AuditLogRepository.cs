using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class AuditLogDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int EntityID { get; set; }
        public string EntityName { get; set; } = null!;

        public int OperationTypeID { get; set; }
        public string OperationTypeName { get; set; } = null!;

        public DateTime DateTime { get; set; }
    }

    public class AuditLogRepository
    {
        private static AuditLogDTO _MapAuditLogDTO(SqlDataReader reader)
        {
            return new AuditLogDTO
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

        public static int AddAuditLog(AuditLogDTO auditLog)
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

        public static AuditLogDTO? GetAuditLogByID(int id)
        {
            AuditLogDTO? auditLog = null;

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

        public static List<AuditLogDTO> GetAllAuditLogs()
        {
            List<AuditLogDTO> auditLogs = new List<AuditLogDTO>();

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

        public static List<AuditLogDTO> GetAuditLogsByUserID(int userId)
        {
            List<AuditLogDTO> auditLogs = new List<AuditLogDTO>();

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

        public static List<AuditLogDTO> SearchAuditLogs(string searchText)
        {
            List<AuditLogDTO> auditLogs = new List<AuditLogDTO>();

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

        public static List<AuditLogDTO> FilterAuditLogs(int? entityId, int? operationTypeId)
        {
            List<AuditLogDTO> auditLogs = new List<AuditLogDTO>();

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
