using System.Linq.Expressions;

namespace WitcherProject.Infrastructure.Query;

public abstract class Query<TEntity>: IQuery<TEntity> where TEntity: class
{
    protected IQueryable<TEntity> _query;
    public abstract Task<IEnumerable<TEntity>> ExecuteAsync();

    public IQuery<TEntity> Filter(Expression<Func<TEntity, bool>> condition)
    {
        _query = _query.Where(condition);
        return this;
    }

    public IQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition, bool ascending = true)
    {
        _query = ascending switch
        {
            true => _query.OrderBy(condition),
            false => _query.OrderByDescending(condition)
        };
        return this;
    }

    public IQuery<TEntity> Page(int page, int pageSize = 10)
    {
        _query = _query.Skip((page - 1) * pageSize).Take(pageSize);
        return this;
    }
}