using WitcherProject.Data.Models;
using WitcherProject.Repositories;

namespace WitcherProject.UnitsOfWork;

public interface IUnitOfWork: IAsyncDisposable
{
    public IGenericRepository<Contract> ContractRepository { get; }
    
    public IGenericRepository<Contractor> ContractorRepository { get; }
    
    public IGenericRepository<ContractRequest> ContractRequestRepository { get; }
    
    public IGenericRepository<Person> PersonRepository { get; }
    
    public IGenericRepository<Role> RoleRepository { get; }

    public IGenericRepository<RoleToPerson> RoleToPersonRepository { get; }
}