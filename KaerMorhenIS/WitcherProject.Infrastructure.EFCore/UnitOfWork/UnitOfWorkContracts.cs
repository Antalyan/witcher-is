using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.Repository;
using WitcherProject.Infrastructure.UnitOfWork;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

// UoW use cases (or where will the UoW be used) - copied bullet points from readme.md:
// witcher to create and view contracts
// witcher to update contract states
// contract manager to add, edit and delete contracts
// contract manager to assign a contract to a witcher

// based on lab04 solution and on https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-unit-of-work-class

public class UnitOfWorkContracts : IUnitOfWorkContracts
{
    private readonly KaerMorhenDBContext _context;

    private IGenericRepository<Person> _personRepository;
    private IGenericRepository<Contract> _contractRepository;
    private IGenericRepository<Contractor> _contractorRepository;
    private IGenericRepository<ContractRequest> _contractRequestRepository;

    public UnitOfWorkContracts(KaerMorhenDBContext context)
    {
        _context = context;
    }
    
    public IGenericRepository<Contract> ContractRepository
    {
        get
        {
            if (this._contractRepository == null)
            {
                this._contractRepository = new EFGenericRepository<Contract>(_context);
            }

            return _contractRepository;
        }
    }

    public IGenericRepository<Contractor> ContractorRepository
    {
        get
        {
            if (this._contractorRepository == null)
            {
                this._contractorRepository = new EFGenericRepository<Contractor>(_context);
            }

            return _contractorRepository;
        }
    }

    public IGenericRepository<ContractRequest> ContractRequestRepository
    {
        get
        {
            if (this._contractRequestRepository == null)
            {
                this._contractRequestRepository = new EFGenericRepository<ContractRequest>(_context);
            }

            return _contractRequestRepository;
        }
    }

    public IGenericRepository<Person> PersonRepository
    {
        get
        {
            if (this._personRepository == null)
            {
                this._personRepository = new EFGenericRepository<Person>(_context);
            }

            return _personRepository;
        }
    }

    
    private bool _disposed = false;
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                await _context.DisposeAsync();
            }
        }
        this._disposed = true;
    }
    
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}