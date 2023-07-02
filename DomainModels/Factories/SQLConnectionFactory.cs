using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Factories
{
    public class SQLConnectionFactory
    {
        private readonly string _connectionString;

        public SQLConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
