using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace Sênior.Dados.Context
{
    public class DbConnectionFactory
    {
        private readonly MyDbContext _dbContext;

        public DbConnectionFactory(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbConnection CreateConnection()
        {
            var dbConnection = new MySqlConnection("server=saf-db.cto2ycmkid7u.us-east-1.rds.amazonaws.com;database=PessoaTesteSenior;user=sistema;password=1234");
            dbConnection.Open();
            return dbConnection;
        }
    }
}
