using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWork;

// UoW use cases (or where will the UoW be used) - copied bullet points from readme.md:
// witcher to create and view contracts
// witcher to update contract states
// contract manager to add, edit and delete contracts
// contract manager to assign a contract to a witcher

// based on lab04 solution and on https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-unit-of-work-class

public class UnitOfWorkContracts : IUnitOfWorkContracts
{
    private readonly DbContext _context;

    public UnitOfWorkContracts(DbContext context, 
        IGenericRepository<Person> personRepository,
        IGenericRepository<Contract> contractRepository, 
        IGenericRepository<Contractor> contractorRepository,
        IGenericRepository<ContractRequest> contractRequestRepository)
    {
        _context = context;
        PersonRepository = personRepository;
        ContractRepository = contractRepository;
        ContractorRepository = contractorRepository;
        ContractRequestRepository = contractRequestRepository;
    }

    public IGenericRepository<Contract> ContractRepository { get; }

    public IGenericRepository<Contractor> ContractorRepository { get; }

    public IGenericRepository<ContractRequest> ContractRequestRepository { get; }

    public IGenericRepository<Person> PersonRepository { get; }

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