using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Infrastructure.Data
{
    public class ApplicationDBContext : IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        public ApplicationDBContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }
        public IDbConnection Connection { 
            get { 
                if(_connection.State == ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection; 
            } 
        }

        public void Dispose()
        {
            if(_connection != null && _connection.State != ConnectionState.Open) { 
                _connection.Close(); 
                _connection.Dispose();
            }
        }
    }
}
