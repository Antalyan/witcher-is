using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

public interface IUnitOfWorkPersonalData : IAsyncDisposable
{
    public IGenericRepository<Person> PersonRepository { get; }
    public IGenericRepository<Role> RoleRepository { get; }
    public IGenericRepository<RoleToPerson> RoleToPersonRepository { get; }
    Task CommitAsync();
}