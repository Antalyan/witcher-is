using WitcherProject.DAL.Data;
using WitcherProject.DAL.Data.Models;
using WitcherProject.DAL.Repositories;
using WitcherProject.Infrastructure.UnitsOfWork;

namespace WitcherProject.UnitsOfWork;

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
        //TODO: IDK what it does or what it is good for
        await _context.DisposeAsync();
    }
}