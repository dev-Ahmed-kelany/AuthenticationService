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

    public class RoleRepository
    {
        public static int AddRole(string name, long permissionsMask)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@PermissionsMask", permissionsMask);

                    //-- Add Output Parameter --//
                    SqlParameter outputNewRoleID = new SqlParameter("@NewRoleID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputNewRoleID);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@NewRoleID"].Value;

                }
            }
        }

        public static bool UpdateRoleByID(int id, string name, long permissionsMask)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateRoleByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@PermissionsMask", permissionsMask);

                    //-- Add Output Parameter --//
                    SqlParameter outputRowsAffected = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputRowsAffected);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@RowsAffected"].Value == 1;

                }
            }
        }

        public static List<RoleDTO> SearchRolesByName(string searchText)
        {
            List<RoleDTO> roles = new List<RoleDTO>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchRolesByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoleDTO role = new RoleDTO();

                            role.ID = (int)reader["ID"];
                            role.Name = (string)reader["Name"];
                            role.PermissionsMask = (long)reader["PermissionsMask"];

                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }

        public static RoleDTO? GetRoleByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetRoleByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RoleDTO
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                PermissionsMask = (long)reader["PermissionsMask"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<RoleDTO> GetAllRoles()
        {
            List<RoleDTO> roles = new List<RoleDTO>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoleDTO role = new RoleDTO();

                            role.ID = (int)reader["ID"];
                            role.Name = (string)reader["Name"];
                            role.PermissionsMask = (long)reader["PermissionsMask"];

                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }

    }
}
