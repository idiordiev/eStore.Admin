using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetAllWithOrderItemsPagedAsync(PagingParameters pagingParameters,
        bool trackChanges,
        CancellationToken cancellationToken)
    {
        var entities = DbSet
            .OrderBy(o => o.Id)
            .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .Include(o => o.OrderItems);
        return trackChanges
            ? await entities
                .ToListAsync(cancellationToken)
            : await entities
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetByConditionWithOrderItemsPagedAsync(
        Expression<Func<Order, bool>> condition,
        PagingParameters pagingParameters, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var entities = DbSet
            .Where(condition)
            .OrderBy(o => o.Id)
            .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .Include(o => o.OrderItems);
        return trackChanges
            ? await entities
                .ToListAsync(cancellationToken)
            : await entities
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    public async Task<Order> GetByIdWithOrderItemsAsync(int id, bool trackChanges,
        CancellationToken cancellationToken)
    {
        return trackChanges
            ? await DbSet
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
            : await DbSet
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}