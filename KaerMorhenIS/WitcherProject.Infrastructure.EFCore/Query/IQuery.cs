using System.Linq.Expressions;

namespace WitcherProject.Infrastructure.Query;

public interface IQuery<TEntity> where TEntity: class
{
    Task<IEnumerable<TEntity>> ExecuteAsync();
    IQuery<TEntity> Filter(Expression<Func<TEntity, bool>> condition);
    IQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition, bool ascending = true);
    IQuery<TEntity> Page(int page, int pageSize = 10);
    IQuery<TEntity> Include(string attribute);
}