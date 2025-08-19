using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

namespace WitcherProject.Infrastructure.EFCore.Repository;

public class EFRepositoryProvider : IRepositoryProvider
{
    public IGenericRepository<T> GetRepository<T>(IUnitOfWork unitOfWork) where T : class
    {
        return new EFGenericRepository<T>(unitOfWork);
    }
}