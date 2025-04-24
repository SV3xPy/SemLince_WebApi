using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SemLince_Infrastructure
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;
        
        public SqlConnectionFactory(IConfiguration configuration)
        {
            //Coneccion BD SqlServer
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }

}
