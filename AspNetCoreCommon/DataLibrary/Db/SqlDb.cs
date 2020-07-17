using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Db
{
    public class SqlDb : IDataAccess
    {
        private IConfiguration _config;

        public SqlDb(IConfiguration config)
        {
            // config comes from front end's dependency injection
            _config = config;
        }


        // Get content from SQL
        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            // Create a connection to the sql server
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // By using 'using', the connection will close after the scope

                // Take store sp and put it into a parameter (safe) to be sent as a store procedure command
                // this prevent a lot of sql injections
                var rows = await connection.QueryAsync<T>(storedProcedure,
                                                        parameters,
                                                        commandType: CommandType.StoredProcedure);

                return rows.ToList();

            }
        }

        public async Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            // Difference from this to  LoadData is that this is not expected to return anything (beside int)
            string connectionString = _config.GetConnectionString(connectionStringName);

            // Create a connection to the sql server
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(storedProcedure,
                                                     parameters,
                                                     commandType: CommandType.StoredProcedure);
            }
        }
    }
}
