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

        public static bool UpdateUserByID(int ID, string Name, string Username, string Email
            , int RoleID, int StatusID)
        {
            string Query = "SP_UpdateUserByID";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    Command.Parameters.AddWithValue("@UserID", ID);
                    Command.Parameters.AddWithValue("@Name", Name);
                    Command.Parameters.AddWithValue("@Username", Username);
                    Command.Parameters.AddWithValue("@Email", Email);
                    Command.Parameters.AddWithValue("@RoleID", RoleID);
                    Command.Parameters.AddWithValue("@StatusID", StatusID);

                    //-- Add Output Parameter --//
                    SqlParameter OutputRowsAffected = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputRowsAffected);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    int RowsAffected = (int)Command.Parameters["@RowsAffected"].Value;

                    return RowsAffected == 1;

                }
            }
        }
    }
}
