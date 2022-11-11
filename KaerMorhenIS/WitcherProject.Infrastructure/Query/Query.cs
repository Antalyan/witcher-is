using System.Linq.Expressions;

namespace WitcherProject.Infrastructure.Query;

public abstract class Query<TEntity>: IQuery<TEntity> where TEntity: class
{
    
}