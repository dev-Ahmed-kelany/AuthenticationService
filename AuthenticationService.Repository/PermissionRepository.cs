using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class PermissionDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public long BitValue { get; set; }
    }

    public class PermissionRepository
    {
        public static int AddPermission(string name, long bitValue)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddNewPermission", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@BitValue", bitValue);

                    //-- Add Output Parameter --//
                    SqlParameter outputNewPermissionID = new SqlParameter("@NewPermissionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputNewPermissionID);

                    connection.Open();

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@NewPermissionID"].Value;

                }
            }
        }

        public static bool UpdatePermissionByID(int id, string name)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdatePermissionByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);

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

        public static List<PermissionDTO> SearchPermissionsByName(string searchText)
        {
            List<PermissionDTO> permissions = new List<PermissionDTO>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_SearchPermissionsByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SearchText", searchText);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PermissionDTO permission = new PermissionDTO();

                            permission.ID = (int)reader["ID"];
                            permission.Name = (string)reader["Name"];
                            permission.BitValue = (long)reader["BitValue"];

                            permissions.Add(permission);
                        }
                    }
                }
            }

            return permissions;
        }

        public static PermissionDTO? GetPermissionByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPermissionByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PermissionDTO
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                BitValue = (long)reader["BitValue"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<PermissionDTO> GetAllPermissions()
        {
            List<PermissionDTO> permissions = new List<PermissionDTO>();

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllPermissions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PermissionDTO permission = new PermissionDTO();

                            permission.ID = (int)reader["ID"];
                            permission.Name = (string)reader["Name"];
                            permission.BitValue = (long)reader["BitValue"];

                            permissions.Add(permission);
                        }
                    }
                }
            }

            return permissions;
        }
    }
}
