using Sênior.Business.Models;

namespace Sênior.Business.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetByCode(int Id);
        Task<Person> GetByCPF(string Id);
        Task<List<Person>> GetByUf(string uf);
        Task<bool> Add(PersonCreate person);
        Task<bool> Update(Person person);
        Task<bool> Remove(int id);
        Task<bool> login(LoginUser login);
        Task<List<Person>> GetAll();


    }
}
