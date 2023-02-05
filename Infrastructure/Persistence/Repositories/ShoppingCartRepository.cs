using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore_Admin.Infrastructure.Persistence.Repositories;

public class ShoppingCartRepository : RepositoryBase<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ShoppingCart>> GetAllWithItemsPagedAsync(PagingParameters pagingParameters,
        bool trackChanges, CancellationToken cancellationToken)
    {
        var entities = DbSet
            .OrderBy(sc => sc.Id)
            .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .Include(sc => sc.Goods);
        return trackChanges
            ? await entities
                .ToListAsync(cancellationToken)
            : await entities
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ShoppingCart>> GetByConditionWithItemsPagedAsync(
        Expression<Func<ShoppingCart, bool>> condition, PagingParameters pagingParameters, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var entities = DbSet
            .Where(condition)
            .OrderBy(sc => sc.Id)
            .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .Include(sc => sc.Goods);
        return trackChanges
            ? await entities
                .ToListAsync(cancellationToken)
            : await entities
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    public async Task<ShoppingCart> GetByIdWithItemsAsync(int id, bool trackChanges,
        CancellationToken cancellationToken)
    {
        return trackChanges
            ? await DbSet
                .Include(sc => sc.Goods)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
            : await DbSet
                .AsNoTracking()
                .Include(sc => sc.Goods)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}