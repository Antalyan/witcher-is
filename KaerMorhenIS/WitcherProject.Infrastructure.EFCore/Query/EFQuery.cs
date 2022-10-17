using Microsoft.EntityFrameworkCore;
using WitcherProject.Infrastructure.Query;

namespace WitcherProject.Infrastructure.EFCore.Query;

public class EFQuery<TEntity>: Query<TEntity> where TEntity: class
{
    private readonly DbContext _context;

    public EFQuery(DbContext dbContext)
    {
        _context = dbContext;
        _query = _context.Set<TEntity>().AsQueryable();
    }

    public override async Task<IEnumerable<TEntity>> ExecuteAsync()
    {
        return await _query.ToListAsync();
    }
}