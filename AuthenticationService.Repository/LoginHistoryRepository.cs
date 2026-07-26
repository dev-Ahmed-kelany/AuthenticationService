using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.LoginHistory;

namespace AuthenticationService.Repository
{
    public class LoginHistoryRepository
    {
        public static int AddLoginHistory(CreateLoginHistoryDto loginHistory)
        {
            int newLoginHistoryID = -1;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddLoginHistory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID",
                        loginHistory.UserID.HasValue ? loginHistory.UserID.Value : DBNull.Value);

                    command.Parameters.AddWithValue("@Status", loginHistory.Status);

                    command.Parameters.AddWithValue("@FailureReason",
                        string.IsNullOrWhiteSpace(loginHistory.FailureReason)
                        ? DBNull.Value
                        : loginHistory.FailureReason);

                    command.Parameters.AddWithValue("@IPAddress",
                        string.IsNullOrWhiteSpace(loginHistory.IPAddress)
                        ? DBNull.Value
                        : loginHistory.IPAddress);

                    command.Parameters.AddWithValue("@Device",
                        string.IsNullOrWhiteSpace(loginHistory.Device)
                        ? DBNull.Value
                        : loginHistory.Device);

                    command.Parameters.AddWithValue("@Browser",
                        string.IsNullOrWhiteSpace(loginHistory.Browser)
                        ? DBNull.Value
                        : loginHistory.Browser);

                    SqlParameter outputParameter = new SqlParameter("@NewLoginHistoryID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputParameter);

                    connection.Open();

                    command.ExecuteNonQuery();

                    newLoginHistoryID = (int)outputParameter.Value;
                }
            }

            return newLoginHistoryID;
        }

        private static LoginHistoryDetailsDto _MapLoginHistoryDTO(SqlDataReader reader)
        {
            return new LoginHistoryDetailsDto
            {
                ID = (int)reader["ID"],
                UserID = reader["UserID"] == DBNull.Value ? null : (int?)reader["UserID"],
                Username = reader["Username"] == DBNull.Value ? null : (string)reader["Username"],
                Name = reader["Name"] == DBNull.Value ? null : (string)reader["Name"],
                Status = (byte)reader["Status"],
                FailureReason = reader["FailureReason"] == DBNull.Value ? null : (string)reader["FailureReason"],
                IPAddress = reader["IPAddress"] == DBNull.Value ? null : (string)reader["IPAddress"],
                Device = reader["Device"] == DBNull.Value ? null : (string)reader["Device"],
                Browser = reader["Browser"] == DBNull.Value ? null : (string)reader["Browser"],
                DateTime = (DateTime)reader["DateTime"]
            };
        }

        public static LoginHistoryDetailsDto? GetLoginHistoryByID(int id)
        {
            LoginHistoryDetailsDto? loginHistory = null;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetLoginHistoryByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loginHistory = _MapLoginHistoryDTO(reader);
                        }
                    }
                }
            }

            return loginHistory;
        }

        public static List<LoginHistoryDetailsDto> GetAllLoginHistory()
        {
            List<LoginHistoryDetailsDto> loginHistoryList = new List<LoginHistoryDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllLoginHistory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loginHistoryList.Add(_MapLoginHistoryDTO(reader));
                        }
                    }
                }
            }

            return loginHistoryList;
        }

        public static List<LoginHistoryDetailsDto> GetLoginHistoryByUserID(int userId)
        {
            List<LoginHistoryDetailsDto> loginHistoryList = new List<LoginHistoryDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetLoginHistoryByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loginHistoryList.Add(_MapLoginHistoryDTO(reader));
                        }
                    }
                }
            }

            return loginHistoryList;
        }

        public static List<LoginHistoryDetailsDto> SearchLoginHistory(string searchText)
        {
            List<LoginHistoryDetailsDto> loginHistoryList = new List<LoginHistoryDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchLoginHistory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loginHistoryList.Add(_MapLoginHistoryDTO(reader));
                        }
                    }
                }
            }

            return loginHistoryList;
        }

        public static List<LoginHistoryDetailsDto> FilterLoginHistoryByStatus(byte loginStatus)
        {
            List<LoginHistoryDetailsDto> loginHistoryList = new List<LoginHistoryDetailsDto>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FilterLoginHistoryByStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Status", loginStatus);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loginHistoryList.Add(_MapLoginHistoryDTO(reader));
                        }
                    }
                }
            }

            return loginHistoryList;
        }

    }
}
