using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace SimpleBotCore.Repositories
{
    public class SqlAskRepository : IAskRepository
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
