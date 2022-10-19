namespace WitcherProject.Infrastructure.UnitOfWork;

public interface IUnitOfWorkAuthentication : IAsyncDisposable
{
    Task CommitAsync();
}