namespace WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

public interface IUnitOfWorkProvider
{
    IUnitOfWork CreateUow();
    IUnitOfWork GetUow();
}