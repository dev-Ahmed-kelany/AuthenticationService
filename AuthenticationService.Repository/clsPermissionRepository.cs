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

    public class clsPermissionRepository
    {
        public static int AddNewPermission(string Name, long BitValue)
        {
            string Query = "SP_AddNewPermission";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    Command.Parameters.AddWithValue("@Name", Name);
                    Command.Parameters.AddWithValue("@BitValue", BitValue);

                    //-- Add Output Parameter --//
                    SqlParameter OutputNewPermissionID = new SqlParameter("@NewPermissionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputNewPermissionID);

                    Connection.Open();

                    Command.ExecuteNonQuery();

                    int NewPermissionID = (int)Command.Parameters["@NewPermissionID"].Value;

                    return NewPermissionID;

                }
            }
        }

        public static bool UpdatePermissionByID(int ID, string Name)
        {
            string Query = "SP_UpdatePermissionByID";

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    //-- Add Parameters --//
                    Command.Parameters.AddWithValue("@ID", ID);
                    Command.Parameters.AddWithValue("@Name", Name);

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

        public static List<PermissionDTO> SearchPermissionsByName(string SearchText)
        {
            List<PermissionDTO> Permissions = new List<PermissionDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_SearchPermissionsByName", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@SearchText", SearchText);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            PermissionDTO Permission = new PermissionDTO();

                            Permission.ID = (int)Reader["ID"];
                            Permission.Name = (string)Reader["Name"];
                            Permission.BitValue = (long)Reader["BitValue"];

                            Permissions.Add(Permission);
                        }
                    }
                }
            }

            return Permissions;
        }

        public static PermissionDTO? GetPermissionByID(int ID)
        {
            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetPermissionByID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ID", ID);

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            return new PermissionDTO
                            {
                                ID = (int)Reader["ID"],
                                Name = (string)Reader["Name"],
                                BitValue = (long)Reader["BitValue"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<PermissionDTO> GetAllPermissions()
        {
            List<PermissionDTO> Permissions = new List<PermissionDTO>();

            using (SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("SP_GetAllPermissions", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();

                    using (SqlDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            PermissionDTO Permission = new PermissionDTO();

                            Permission.ID = (int)Reader["ID"];
                            Permission.Name = (string)Reader["Name"];
                            Permission.BitValue = (long)Reader["BitValue"];

                            Permissions.Add(Permission);
                        }
                    }
                }
            }

            return Permissions;
        }
    }
}
