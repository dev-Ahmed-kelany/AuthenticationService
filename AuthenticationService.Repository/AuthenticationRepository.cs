using System.Data;
using Microsoft.Data.SqlClient;
using AuthenticationService.Dtos.Authentication;

namespace AuthenticationService.Repository
{
    public class AuthenticationRepository
    {
        public static bool GetAuthenticationUserByUsername(string username, ref AuthenticationUserDto user)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAuthenticationUserByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                user.ID = (int)reader["ID"];
                                user.Username = (string)reader["Username"];
                                user.PasswordHash = (string)reader["PasswordHash"];
                                user.RoleID = (int)reader["RoleID"];
                                user.StatusID = (int)reader["StatusID"];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool ChangePassword(int userId, string newPasswordHash)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_ChangePassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);

                    SqlParameter rowsAffectedParameter = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(rowsAffectedParameter);

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        rowsAffected = (int)rowsAffectedParameter.Value;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

    }
}
