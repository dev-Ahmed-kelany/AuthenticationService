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

    public class clsAuditLogRepository
    {
        private static AuditLogDTO _MapAuditLogDTO(SqlDataReader Reader)
        {
            return new AuditLogDTO
            {
                ID = (int)Reader["ID"],
                UserID = (int)Reader["UserID"],
                Username = (string)Reader["Username"],
                Name = (string)Reader["Name"],
                EntityID = (int)Reader["EntityID"],
                EntityName = (string)Reader["EntityName"],
                OperationTypeID = (int)Reader["OperationTypeID"],
                OperationTypeName = (string)Reader["OperationTypeName"],
                DateTime = (DateTime)Reader["DateTime"]
            };
        }

        public static int AddNewAuditLog(AuditLogDTO AuditLog)
        {
            int NewAuditLogID = -1;

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_AddNewAuditLog", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@UserID", AuditLog.UserID);
                    Command.Parameters.AddWithValue("@EntityID", AuditLog.EntityID);
                    Command.Parameters.AddWithValue("@OperationTypeID", AuditLog.OperationTypeID);

                    SqlParameter OutputParameter = new SqlParameter("@NewAuditLogID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    Command.Parameters.Add(OutputParameter);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    NewAuditLogID = (int)OutputParameter.Value;
                }
            }

            return NewAuditLogID;
        }

        public static AuditLogDTO? GetAuditLogByID(int ID)
        {
            AuditLogDTO? AuditLog = null;

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetAuditLogByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ID", ID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                            AuditLog = _MapAuditLogDTO(Reader);
                    }
                }
            }

            return AuditLog;
        }

        public static List<AuditLogDTO> GetAllAuditLogs()
        {
            List<AuditLogDTO> AuditLogs = new List<AuditLogDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetAllAuditLogs", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            AuditLogs.Add(_MapAuditLogDTO(Reader));
                        }
                    }
                }
            }

            return AuditLogs;
        }

        public static List<AuditLogDTO> GetAuditLogsByUserID(int UserID)
        {
            List<AuditLogDTO> AuditLogs = new List<AuditLogDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetAuditLogsByUserID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@UserID", UserID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            AuditLogs.Add(_MapAuditLogDTO(Reader));
                        }
                    }
                }
            }

            return AuditLogs;
        }

        public static List<AuditLogDTO> SearchAuditLogs(string SearchText)
        {
            List<AuditLogDTO> AuditLogs = new List<AuditLogDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_SearchAuditLogs", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@SearchText", SearchText);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            AuditLogs.Add(_MapAuditLogDTO(Reader));
                        }
                    }
                }
            }

            return AuditLogs;
        }

        public static List<AuditLogDTO> FilterAuditLogs(int? EntityID, int? OperationTypeID)
        {
            List<AuditLogDTO> AuditLogs = new List<AuditLogDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_FilterAuditLogs", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@EntityID",
                        EntityID.HasValue ? EntityID.Value : DBNull.Value);

                    Command.Parameters.AddWithValue("@OperationTypeID",
                        OperationTypeID.HasValue ? OperationTypeID.Value : DBNull.Value);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            AuditLogs.Add(_MapAuditLogDTO(Reader));
                        }
                    }
                }
            }

            return AuditLogs;
        }

    }
}
