using Microsoft.EntityFrameworkCore;
using WitcherProject.Data;

namespace WitcherProject.Repositories;

public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity: class
{
    private readonly KaerMorhenDBContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(KaerMorhenDBContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    public async Task Delete(int id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _context.Attach(entityToDelete);
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
        _context.Entry(entity).State = EntityState.Modified;
    }
}