using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL.Data;

namespace WitcherProject.DAL.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Delete(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Insert(TEntity entity);
    void Update(TEntity entity);
}