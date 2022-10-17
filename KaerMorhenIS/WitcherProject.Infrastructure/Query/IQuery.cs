using System.Linq.Expressions;

namespace WitcherProject.Infrastructure.Query;

public interface IQuery<TEntity> where TEntity: class
{
    Task<IEnumerable<TEntity>> ExecuteAsync();
    Query<TEntity> Filter(Expression<Func<TEntity, bool>> condition);
    Query<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition, bool ascending = true);
    Query<TEntity> Page(int page, int pageSize);
}