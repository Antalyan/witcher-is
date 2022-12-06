using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Repository;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

public class EFUnitOfWorkProvider: IUnitOfWorkProvider
{
    private IDbContextFactory<KaerMorhenDBContext> _factory;
    public EFUnitOfWorkProvider(IDbContextFactory<KaerMorhenDBContext> factory)
    {
        _factory = factory;
    }
    
    private KaerMorhenDBContext? _context;
    private EFUnitOfWork _uow;
    
    public IUnitOfWork CreateUow()
    {
        _context = _factory.CreateDbContext();
        _uow = new EFUnitOfWork(_context);
        return _uow;
    }
    
    public IUnitOfWork GetUow()
    {
        return _uow;
    }
}