﻿using WitcherProject.DAL;

namespace WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;

public class EFUnitOfWork: IUnitOfWork
{
    public KaerMorhenDBContext Context { get; }
    public EFUnitOfWork(KaerMorhenDBContext context)
    {
        Context = context;
    }
    
    private bool _disposed;

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                await Context.DisposeAsync();
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
        await Context.SaveChangesAsync();
    }
}