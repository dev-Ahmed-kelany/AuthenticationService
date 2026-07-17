using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class LoginHistoryDTO
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public string? Username { get; set; }

        public string? Name { get; set; }

        public byte Status { get; set; }

        public string? FailureReason { get; set; }

        public string? IPAddress { get; set; }

        public string? Device { get; set; }

        public string? Browser { get; set; }

        public DateTime DateTime { get; set; }
    }

    public class clsLoginHistoryRepository
    {
        public static int AddNewLoginHistory(LoginHistoryDTO LoginHistory)
        {
            int NewLoginHistoryID = -1;

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_AddLoginHistory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@UserID",
                        LoginHistory.UserID.HasValue ? LoginHistory.UserID.Value : DBNull.Value);

                    Command.Parameters.AddWithValue("@Status", LoginHistory.Status);

                    Command.Parameters.AddWithValue("@FailureReason",
                        string.IsNullOrWhiteSpace(LoginHistory.FailureReason)
                        ? DBNull.Value
                        : LoginHistory.FailureReason);

                    Command.Parameters.AddWithValue("@IPAddress",
                        string.IsNullOrWhiteSpace(LoginHistory.IPAddress)
                        ? DBNull.Value
                        : LoginHistory.IPAddress);

                    Command.Parameters.AddWithValue("@Device",
                        string.IsNullOrWhiteSpace(LoginHistory.Device)
                        ? DBNull.Value
                        : LoginHistory.Device);

                    Command.Parameters.AddWithValue("@Browser",
                        string.IsNullOrWhiteSpace(LoginHistory.Browser)
                        ? DBNull.Value
                        : LoginHistory.Browser);

                    SqlParameter OutputParameter = new SqlParameter("@NewLoginHistoryID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    Command.Parameters.Add(OutputParameter);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    NewLoginHistoryID = (int)OutputParameter.Value;
                }
            }

            return NewLoginHistoryID;
        }

        private static LoginHistoryDTO _MapLoginHistoryDTO(SqlDataReader Reader)
        {
            return new LoginHistoryDTO
            {
                ID = (int)Reader["ID"],
                UserID = Reader["UserID"] == DBNull.Value ? null : (int?)Reader["UserID"],
                Username = Reader["Username"] == DBNull.Value ? null : (string)Reader["Username"],
                Name = Reader["Name"] == DBNull.Value ? null : (string)Reader["Name"],
                Status = (byte)Reader["Status"],
                FailureReason = Reader["FailureReason"] == DBNull.Value ? null : (string)Reader["FailureReason"],
                IPAddress = Reader["IPAddress"] == DBNull.Value ? null : (string)Reader["IPAddress"],
                Device = Reader["Device"] == DBNull.Value ? null : (string)Reader["Device"],
                Browser = Reader["Browser"] == DBNull.Value ? null : (string)Reader["Browser"],
                DateTime = (DateTime)Reader["DateTime"]
            };
        }

        public static LoginHistoryDTO? GetLoginHistoryByID(int ID)
        {
            LoginHistoryDTO? LoginHistory = null;

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetLoginHistoryByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@ID", ID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            LoginHistory = _MapLoginHistoryDTO(Reader);
                        }
                    }
                }
            }

            return LoginHistory;
        }

        public static List<LoginHistoryDTO> GetAllLoginHistory()
        {
            List<LoginHistoryDTO> LoginHistoryList = new List<LoginHistoryDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetAllLoginHistory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            LoginHistoryList.Add(_MapLoginHistoryDTO(Reader));
                        }
                    }
                }
            }

            return LoginHistoryList;
        }

        public static List<LoginHistoryDTO> GetLoginHistoryByUserID(int UserID)
        {
            List<LoginHistoryDTO> LoginHistoryList = new List<LoginHistoryDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetLoginHistoryByUserID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            LoginHistoryList.Add(_MapLoginHistoryDTO(Reader));
                        }
                    }
                }
            }

            return LoginHistoryList;
        }

        public static List<LoginHistoryDTO> SearchLoginHistory(string SearchText)
        {
            List<LoginHistoryDTO> LoginHistoryList = new List<LoginHistoryDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_SearchLoginHistory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@SearchText", SearchText);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            LoginHistoryList.Add(_MapLoginHistoryDTO(Reader));
                        }
                    }
                }
            }

            return LoginHistoryList;
        }

        public static List<LoginHistoryDTO> FilterLoginHistoryByStatus(byte LoginStatus)
        {
            List<LoginHistoryDTO> LoginHistoryList = new List<LoginHistoryDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_FilterLoginHistoryByStatus", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@Status", LoginStatus);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            LoginHistoryList.Add(_MapLoginHistoryDTO(Reader));
                        }
                    }
                }
            }

            return LoginHistoryList;
        }


    }
}
