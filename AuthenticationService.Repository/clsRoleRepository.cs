using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class RoleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public long PermissionsMask { get; set; }
    }

    public class clsRoleRepository
    {
        public static int AddNewRole(string Name, long PermissionsMask)
        {
            string Query = "SP_AddNewRole";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    Command.Parameters.AddWithValue("@Name", Name);
                    Command.Parameters.AddWithValue("@PermissionsMask", PermissionsMask);

                    //-- Add Output Parameter --//
                    SqlParameter OutputNewRoleID = new SqlParameter("@NewRoleID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputNewRoleID);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    int NewRoleID = (int)Command.Parameters["@NewRoleID"].Value;

                    return NewRoleID;

                }
            }
        }

        public static bool UpdateRoleByID(int ID, string Name, long PermissionsMask)
        {
            string Query = "SP_UpdateRoleByID";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    Command.Parameters.AddWithValue("@ID", ID);
                    Command.Parameters.AddWithValue("@Name", Name);
                    Command.Parameters.AddWithValue("@PermissionsMask", PermissionsMask);

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

        public static List<RoleDTO> SearchRolesByName(string SearchText)
        {
            List<RoleDTO> Roles = new List<RoleDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_SearchRolesByName", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@SearchText", SearchText);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            RoleDTO Role = new RoleDTO();

                            Role.ID = (int)Reader["ID"];
                            Role.Name = (string)Reader["Name"];
                            Role.PermissionsMask = (long)Reader["PermissionsMask"];

                            Roles.Add(Role);
                        }
                    }
                }
            }

            return Roles;
        }

        public static RoleDTO? GetRoleByID(int ID)
        {
            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetRoleByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ID", ID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            return new RoleDTO
                            {
                                ID = (int)Reader["ID"],
                                Name = (string)Reader["Name"],
                                PermissionsMask = (long)Reader["PermissionsMask"]
                            };
                        }
                    }
                }
            }

            return null;
        }


    }
}
