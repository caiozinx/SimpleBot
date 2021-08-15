using Microsoft.Extensions.Configuration;
using SimpleBotCore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class SqlAskRepository : ISqlAskRepository
    {
        private readonly IConfiguration _configuration;

        public SqlAskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void StoreAsk(string ask)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SimpleBotCore")))
            {
                connection.Open();
                string query = "INSERT ASK VALUES (@ask)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@ask", ask));
                    command.ExecuteScalar();
                }
            }
        }
    }
}
