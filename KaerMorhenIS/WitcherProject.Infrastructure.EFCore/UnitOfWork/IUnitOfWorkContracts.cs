using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

public interface IUnitOfWorkContracts : IAsyncDisposable
{
    public IGenericRepository<Contract> ContractRepository { get; }
    public IGenericRepository<Contractor> ContractorRepository { get; }
    public IGenericRepository<ContractRequest> ContractRequestRepository { get; }
    public IGenericRepository<Person> PersonRepository { get; }
    Task CommitAsync();
}