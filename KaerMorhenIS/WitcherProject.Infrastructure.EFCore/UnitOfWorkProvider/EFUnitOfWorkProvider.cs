using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

public class EFUnitOfWorkProvider: IUnitOfWorkProvider
{
    private readonly IDbContextFactory<KaerMorhenDBContext> _factory;
    public EFUnitOfWorkProvider(IDbContextFactory<KaerMorhenDBContext> factory)
    {
        _factory = factory;
    }
    
    public IUnitOfWork CreateUow()
    {
        return new EFUnitOfWork(_factory.CreateDbContext());
    }
}