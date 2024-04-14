using Sênior.Business.Models;
using System.Linq.Expressions;

namespace Sênior.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(int id);
        Task<int> SaveChanges();
    }
}
