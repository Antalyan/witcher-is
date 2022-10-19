using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.Repository;
using WitcherProject.Infrastructure.UnitOfWork;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

// UoW use cases (or where will the UoW be used) - copied bullet points from readme.md:
// person manager to assign roles to users
// user to change their personal data

// based on lab04 solution and on https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-unit-of-work-class

public class UnitOfWorkPersonalData : IUnitOfWorkPersonalData
{
    private readonly KaerMorhenDBContext _context;

    private IGenericRepository<Person> _personRepository;
    private IGenericRepository<Role> _roleRepository;
    private IGenericRepository<RoleToPerson> _roleToPersonRepository;

    public UnitOfWorkPersonalData(KaerMorhenDBContext context)
    {
        _context = context;
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

    public IGenericRepository<Role> RoleRepository
    {
        get
        {
            if (this._roleRepository == null)
            {
                this._roleRepository = new EFGenericRepository<Role>(_context);
            }

            return _roleRepository;
        }
    }

    public IGenericRepository<RoleToPerson> RoleToPersonRepository
    {
        get
        {
            if (this._roleToPersonRepository == null)
            {
                this._roleToPersonRepository = new EFGenericRepository<RoleToPerson>(_context);
            }

            return _roleToPersonRepository;
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