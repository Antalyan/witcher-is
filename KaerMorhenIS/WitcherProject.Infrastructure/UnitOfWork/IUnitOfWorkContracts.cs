namespace WitcherProject.Infrastructure.UnitOfWork;

public interface IUnitOfWorkContracts : IAsyncDisposable
{
    Task CommitAsync();
}