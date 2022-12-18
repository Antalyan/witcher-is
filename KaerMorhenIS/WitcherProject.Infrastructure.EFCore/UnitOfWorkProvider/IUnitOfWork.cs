namespace WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

public interface IUnitOfWork: IAsyncDisposable
{
    Task CommitAsync();
}