using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Infrastructure.Query;

namespace WitcherProject.Infrastructure.EFCore.Query;

public class EFQuery<TEntity>: IQuery<TEntity> where TEntity: class
{
    private IDbContextFactory<KaerMorhenDBContext> _contextFactory;
    private KaerMorhenDBContext? _context;
    private IQueryable<TEntity> _query;

    public EFQuery(IDbContextFactory<KaerMorhenDBContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IQuery<TEntity> Filter(Expression<Func<TEntity, bool>> condition)
    {
        CheckOnStart();
        _query = _query.Where(condition);
        return this;
    }

    public IQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition, bool ascending = true)
    {
        CheckOnStart();
        _query = ascending switch
        {
            true => _query.OrderBy(condition),
            false => _query.OrderByDescending(condition)
        };
        return this;
    }

    public IQuery<TEntity> Page(int page, int pageSize = 10)
    {
        CheckOnStart();
        _query = _query.Skip((page - 1) * pageSize).Take(pageSize);
        return this;
    }
    public IQuery<TEntity> Include(string attribute)
    {
        CheckOnStart();
        _query = _query.Include(attribute);
        return this;
    }
    

    private void CheckOnStart()
    {
        if (_context == null)
        {
            _context = _contextFactory.CreateDbContext();
            _query = _context.Set<TEntity>().AsQueryable();
        }
    }

    private async Task End()
    {
        await _context.DisposeAsync();
        _context = null;
    }

    public async Task<IEnumerable<TEntity>> ExecuteAsync()
    {
        var result = await _query.ToListAsync();
        await End();
        return result;
    }
}