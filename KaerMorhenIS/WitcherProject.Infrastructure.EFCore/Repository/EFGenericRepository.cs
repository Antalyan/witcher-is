using Microsoft.EntityFrameworkCore;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.Infrastructure.EFCore.Repository;

public class EFGenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity: class
{
    private readonly EFUnitOfWork _uow;
    private readonly DbSet<TEntity> _dbSet;
    
    public EFGenericRepository(IUnitOfWork uow)
    {
        _uow = (EFUnitOfWork) uow;
        _dbSet = _uow.Context.Set<TEntity>();
    }

    public async Task Delete(int id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        if (_uow.Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _uow.Context.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Insert(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _uow.Context.Entry(entity).State = EntityState.Modified;
    }
}