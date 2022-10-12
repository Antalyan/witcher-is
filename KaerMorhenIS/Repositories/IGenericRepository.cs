using Microsoft.EntityFrameworkCore;
using WitcherProject.Data;

namespace WitcherProject.Repositories;

public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : class
{
    Task Delete(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Insert(TEntity entity);
    void Update(TEntity entity);
}