using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SamplesApp.Data
{
    public class DatabaseCommand : IDatabaseCommand
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["AzureDbConnection"].ConnectionString;

        public DataTable GetDatatableFromQuery(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                var dataTable = new DataTable();
                dataTable.Load(command.ExecuteReader());
                return dataTable;
            }
        }

        public bool ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedureName, connection) { CommandType = CommandType.StoredProcedure })
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                connection.Open();
                return command.ExecuteNonQuery() != 0;
            }
        }
    }
}