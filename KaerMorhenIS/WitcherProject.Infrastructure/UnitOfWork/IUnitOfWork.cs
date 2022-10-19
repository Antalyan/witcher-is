using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.Repository;

namespace WitcherProject.Infrastructure.UnitOfWork;

public interface IUnitOfWork: IAsyncDisposable
{
    Task CommitAsync();
}