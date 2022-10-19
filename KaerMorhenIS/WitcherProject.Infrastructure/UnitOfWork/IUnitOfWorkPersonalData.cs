namespace WitcherProject.Infrastructure.UnitOfWork;

public interface IUnitOfWorkPersonalData : IAsyncDisposable
{
    Task CommitAsync();
}