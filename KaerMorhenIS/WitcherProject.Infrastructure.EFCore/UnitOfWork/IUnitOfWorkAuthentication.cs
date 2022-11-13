using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

public interface IUnitOfWorkAuthentication : IAsyncDisposable
{
    public IGenericRepository<Person> PersonRepository { get; }
    Task CommitAsync();
}