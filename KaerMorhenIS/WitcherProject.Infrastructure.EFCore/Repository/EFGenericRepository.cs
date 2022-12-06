using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.Infrastructure.EFCore.Repository;

public class EFGenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity: class
{
    private KaerMorhenDBContext _context;
    private DbSet<TEntity> _dbSet;
    private readonly IUnitOfWorkProvider _provider;

    public EFGenericRepository(IUnitOfWorkProvider provider)
    {
        _provider = (EFUnitOfWorkProvider) provider;
    }

    private void InitUow()
    {
        var uow = (UnitOfWorkProvider.UnitOfWork) _provider.GetUow();
        _context = uow.Context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task Delete(int id)
    {
        InitUow();
        var entityToDelete = await _dbSet.FindAsync(id);

        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _context.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        InitUow();
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        InitUow();
        return await _dbSet.FindAsync(id);
    }

    public async Task Insert(TEntity entity)
    {
        InitUow();
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        InitUow();
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}