using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AuthenticationService.Repository
{
    public class clsUserRepository
    {
        public static int AddNewUser(string Name, string Username, string Email,
            string PasswordHash, int RoleID, int StatusID)
        {
            string Query = "SP_AddNewUser";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    Command.Parameters.AddWithValue("@Name", Name);
                    Command.Parameters.AddWithValue("@Username", Username);
                    Command.Parameters.AddWithValue("@Email", Email);
                    Command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    Command.Parameters.AddWithValue("@RoleID", RoleID);
                    Command.Parameters.AddWithValue("@StatusID", StatusID);

                    //-- Add Output Parameter --//
                    SqlParameter OutputNewUserID = new SqlParameter("@NewUserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputNewUserID);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    int NewUserID = (int)Command.Parameters["@NewUserID"].Value;

                    return NewUserID;

                }
            }
        }
    }
}
