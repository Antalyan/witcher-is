using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.Repository;
using WitcherProject.Infrastructure.UnitOfWork;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    private KaerMorhenDBContext _context;
    
    public IGenericRepository<Contract> ContractRepository { get; }
    public IGenericRepository<Contractor> ContractorRepository { get; }
    public IGenericRepository<ContractRequest> ContractRequestRepository { get; }
    public IGenericRepository<Person> PersonRepository { get; }
    public IGenericRepository<Role> RoleRepository { get; }
    public IGenericRepository<RoleToPerson> RoleToPersonRepository { get; }

    public UnitOfWork(KaerMorhenDBContext context)
    {
        _context = context;
        ContractRepository = new GenericRepository<Contract>(_context);
        ContractorRepository = new GenericRepository<Contractor>(_context);
        ContractRequestRepository = new GenericRepository<ContractRequest>(_context);
        PersonRepository = new GenericRepository<Person>(_context);
        RoleRepository = new GenericRepository<Role>(_context);
        RoleToPersonRepository = new GenericRepository<RoleToPerson>(_context);
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}