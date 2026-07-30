using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Repository
{
    public class StatusRepository
    {
        public static bool StatusExists(int statusId)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_StatusExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StatusID", statusId);

                    try
                    {
                        connection.Open();

                        exists = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    catch (Exception)
                    {
                        // Error Handling will be added later.
                    }
                }
            }

            return exists;
        }
    }
}
