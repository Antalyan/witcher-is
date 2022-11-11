namespace WitcherProject.Infrastructure.Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Delete(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Insert(TEntity entity);
    void Update(TEntity entity);
}