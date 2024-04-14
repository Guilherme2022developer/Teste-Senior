using Sênior.Business.Models;

namespace Sênior.Business.Services
{
    public interface IPersonService : IDisposable
    {
        Task<bool> Add(PersonCreate person);
        Task<bool> Update(Person person);
        Task<bool> Remove(int id);
        Task <bool> login(LoginUser login);
    }
}
