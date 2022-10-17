using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.Repository;

namespace WitcherProject.Infrastructure.UnitOfWork;

public interface IUnitOfWork: IAsyncDisposable
{
    public IGenericRepository<Contract> ContractRepository { get; }
    
    public IGenericRepository<Contractor> ContractorRepository { get; }
    
    public IGenericRepository<ContractRequest> ContractRequestRepository { get; }
    
    public IGenericRepository<Person> PersonRepository { get; }
    
    public IGenericRepository<Role> RoleRepository { get; }

    public IGenericRepository<RoleToPerson> RoleToPersonRepository { get; }
}