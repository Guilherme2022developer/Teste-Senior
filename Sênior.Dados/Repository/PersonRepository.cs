using Microsoft.EntityFrameworkCore;
using Sênior.Business.Interfaces;
using Sênior.Business.Models;
using Sênior.Dados.Context;
using System.Data;
using Dapper;

namespace Sênior.Dados.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        private readonly MyDbContext _dbContext;

        public PersonRepository(MyDbContext dbContext, DbConnectionFactory connectionFactory) : base(dbContext) 
        {
            _dbContext = dbContext;
            _connectionFactory = connectionFactory;
        }

        public async Task<Person> GetByCode(int Id)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"SELECT * FROM Pessoa P WHERE p.Id = @Id AND p.Active = 1";

            var person = await dbConnection.QueryFirstOrDefaultAsync<Person>(query, new { Id });

            dbConnection.Close();

            return person;
        }
        public async Task<Person> GetByCPF(string cpf)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"SELECT * FROM Pessoa WHERE CPF = @cpf AND Active = 1";

            var person = await dbConnection.QueryFirstOrDefaultAsync<Person>(query, new { cpf });

            dbConnection.Close();

            return person;
        }
        public async Task<List<Person>> GetAll()
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"SELECT * FROM Pessoa WHERE Active = 1";

            var person = await dbConnection.QueryAsync<Person>(query);

            dbConnection.Close();

            return person.ToList();
        }

        public async Task<List<Person>> GetByUf(string uf)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"SELECT * FROM Pessoa WHERE UF = @uf AND Active = 1";

            var person = await dbConnection.QueryAsync<Person>(query, new { uf });

            dbConnection.Close();

            return person.ToList();
        }

        public async Task<bool> Add(PersonCreate person)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"
                        INSERT
                        INTO
                        Pessoa (Name,
                                LastName, 
                                Email, 
                                Telephone, 
                                BirthDate, 
                                Active, 
                                CPF, 
                                UF, 
                                Login,
                                Password)
                         VALUES 
                                (@Name, 
                                @LastName, 
                                @Email, 
                                @Telephone, 
                                @BirthDate, 
                                @Active, 
                                @CPF, 
                                @UF, 
                                @Login,
                                @Password)"
            ;

            var affectedRows = await dbConnection.ExecuteAsync(query, person);

            dbConnection.Close();

            return affectedRows > 0;
        }

        public async Task<bool> Update(Person person)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"UPDATE 
                         Pessoa 
                         SET Name = @Name,
                            LastName = @LastName, 
                            Email = @Email, 
                            Telephone = @Telephone, 
                            BirthDate = @BirthDate, 
                            Active = @Active, 
                            CPF = @CPF, 
                            UF = @UF
                        WHERE Id = @Id"
            ;

            var affectedRows = await dbConnection.ExecuteAsync(query, person);

            dbConnection.Close();

            return affectedRows > 0;
        }

        public async Task<bool> login(LoginUser login)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"SELECT * FROM Pessoa WHERE Login = @Login AND Password = @Password AND Active = 1";

            var person = await dbConnection.QueryFirstOrDefaultAsync<Person>(query, login);

            dbConnection.Close();

            return person != null;
        }

        public async Task<bool> Remove(int id)
        {
            IDbConnection dbConnection = _connectionFactory.CreateConnection();

            var query = @"UPDATE Pessoa SET Active = 0 WHERE Id = @Id";
             
            var affectedRows = await dbConnection.ExecuteAsync(query, new { Id = id });

            dbConnection.Close();

            return affectedRows > 0;
        }

        
    }
}
