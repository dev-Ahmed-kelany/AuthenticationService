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
        public int RoleID { get; set; }
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
    }
}
