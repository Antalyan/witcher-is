using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

// UoW use cases (or where will the UoW be used) - copied bullet points from readme.md:
// person manager to assign roles to users
// user to change their personal data

// based on lab04 solution and on https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-unit-of-work-class

public class UnitOfWorkPersonalData : IUnitOfWorkPersonalData
{
    private readonly DbContext _context;

    public UnitOfWorkPersonalData(DbContext context, 
        IGenericRepository<Person> personRepository, 
        IGenericRepository<Role> roleRepository, 
        IGenericRepository<RoleToPerson> roleToPersonRepository)
    {
        _context = context;
        PersonRepository = personRepository;
        RoleRepository = roleRepository;
        RoleToPersonRepository = roleToPersonRepository;
    }

    public IGenericRepository<Person> PersonRepository { get; }

    public IGenericRepository<Role> RoleRepository { get; }

    public IGenericRepository<RoleToPerson> RoleToPersonRepository { get; }

    private bool _disposed;
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                await _context.DisposeAsync();
            }
        }
        _disposed = true;
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