using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class AuthenticationUserDTO
    {
        public int ID { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public int RoleID { get; set; }

        public int StatusID { get; set; }
    }

    public class clsAuthenticationRepository
    {
        public static bool GetAuthenticationUserByUsername(string Username, ref AuthenticationUserDTO User)
        {
            bool IsFound = false;

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetAuthenticationUserByUsername", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Username", Username);

                    try
                    {
                        Connection.Open();

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                IsFound = true;

                                User.ID = (int)Reader["ID"];
                                User.Username = (string)Reader["Username"];
                                User.PasswordHash = (string)Reader["PasswordHash"];
                                User.RoleID = (int)Reader["RoleID"];
                                User.StatusID = (int)Reader["StatusID"];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        IsFound = false;
                    }
                }
            }

            return IsFound;
        }

        public static bool ChangePassword(int UserID, string NewPasswordHash)
        {
            int RowsAffected = 0;

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_ChangePassword", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@UserID", UserID);
                    Command.Parameters.AddWithValue("@NewPasswordHash", NewPasswordHash);

                    SqlParameter RowsAffectedParameter = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    Command.Parameters.Add(RowsAffectedParameter);

                    try
                    {
                        Connection.Open();

                        Command.ExecuteNonQuery();

                        RowsAffected = (int)RowsAffectedParameter.Value;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (RowsAffected > 0);
        }

    }
}
