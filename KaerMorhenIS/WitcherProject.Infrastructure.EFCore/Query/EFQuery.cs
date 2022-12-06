﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WitcherProject.DAL;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Infrastructure.Query;

namespace WitcherProject.Infrastructure.EFCore.Query;

public class EFQuery<TEntity>: IQuery<TEntity> where TEntity: class
{
    private KaerMorhenDBContext _context;
    private IQueryable<TEntity> _query;

    public EFQuery(IDbContextFactory<KaerMorhenDBContext> contextFactory)
    {
        _context = contextFactory.CreateDbContext();
        _query = _context.Set<TEntity>().AsQueryable();
    }

    public IQuery<TEntity> Filter(Expression<Func<TEntity, bool>> condition)
    {
        _query = _query.Where(condition);
        return this;
    }

    public IQuery<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition, bool ascending = true)
    {
        _query = ascending switch
        {
            true => _query.OrderBy(condition),
            false => _query.OrderByDescending(condition)
        };
        return this;
    }

    public IQuery<TEntity> Page(int page, int pageSize = 10)
    {
        _query = _query.Skip((page - 1) * pageSize).Take(pageSize);
        return this;
    }

    public async Task<IEnumerable<TEntity>> ExecuteAsync()
    {
        return await _query.ToListAsync();
    }
}