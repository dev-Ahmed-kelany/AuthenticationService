using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AuthenticationService.Repository
{
    public class UserDTO
    {
        // Properties
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleID { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }

    }

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

        public static bool DeleteUserByID(int ID)
        {
            const string Query = "SP_DeleteUserByID";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Input Parameter --//
                    Command.Parameters.AddWithValue("@UserID", ID);

                    //-- Output Parameter --//
                    SqlParameter OutputRowsAffected = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    Command.Parameters.Add(OutputRowsAffected);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    return (int)OutputRowsAffected.Value == 1;
                }
            }
        }

        public static List<UserDTO> SearchUsers(string SearchText)
        {
            List<UserDTO> Users = new List<UserDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_SearchUsers", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@SearchText", SearchText);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            UserDTO User = new UserDTO();

                            User.ID = (int)Reader["ID"];
                            User.Name = (string)Reader["Name"];
                            User.Username = (string)Reader["Username"];
                            User.Email = (string)Reader["Email"];
                            User.RoleID = (int)Reader["RoleID"];
                            User.StatusID = (int)Reader["StatusID"];
                            User.CreatedAt = (DateTime)Reader["CreatedAt"];

                            Users.Add(User);
                        }
                    }
                }
            }

            return Users;
        }

        public static List<UserDTO> FilterUsersByRoleID(int RoleID)
        {
            List<UserDTO> Users = new List<UserDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_FilterUsersByRoleID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RoleID", RoleID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            UserDTO User = new UserDTO();

                            User.ID = (int)Reader["ID"];
                            User.Name = (string)Reader["Name"];
                            User.Username = (string)Reader["Username"];
                            User.Email = (string)Reader["Email"];
                            User.RoleID = (int)Reader["RoleID"];
                            User.StatusID = (int)Reader["StatusID"];
                            User.CreatedAt = (DateTime)Reader["CreatedAt"];

                            Users.Add(User);
                        }
                    }
                }
            }

            return Users;
        }

        public static List<UserDTO> FilterUsersByStatusID(int StatusID)
        {
            List<UserDTO> Users = new List<UserDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_FilterUsersByStatusID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StatusID", StatusID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            UserDTO User = new UserDTO();

                            User.ID = (int)Reader["ID"];
                            User.Name = (string)Reader["Name"];
                            User.Username = (string)Reader["Username"];
                            User.Email = (string)Reader["Email"];
                            User.RoleID = (int)Reader["RoleID"];
                            User.StatusID = (int)Reader["StatusID"];
                            User.CreatedAt = (DateTime)Reader["CreatedAt"];

                            Users.Add(User);
                        }
                    }
                }
            }

            return Users;
        }
    }
}
