using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.Infrastructure.EFCore.Repository;

public interface IRepositoryProvider
{
    IGenericRepository<TEntity> GetRepository<TEntity>(IUnitOfWork unitOfWork) where TEntity : class;
}