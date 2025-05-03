using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SemLince_Infrastructure
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionStringSQLServer;
        
        public SqlConnectionFactory(IConfiguration configuration)
        {
            //Coneccion BD SqlServer
            _connectionStringSQLServer = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection CreateSqlServerConnection()
        {
            return new SqlConnection(_connectionStringSQLServer);
        }
    }

}
