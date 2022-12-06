using WitcherProject.Infrastructure.EFCore.UnitOfWork;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

public interface IUnitOfWorkProvider
{
    IUnitOfWork CreateUow();
    IUnitOfWork GetUow();
}