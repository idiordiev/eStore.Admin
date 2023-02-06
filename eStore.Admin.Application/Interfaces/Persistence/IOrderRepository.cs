using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Interfaces.Persistence.Shared;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Interfaces.Persistence;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<IEnumerable<Order>> GetAllWithOrderItemsPagedAsync(PagingParameters pagingParameters, bool trackChanges,
        CancellationToken cancellationToken);

    Task<IEnumerable<Order>> GetByConditionWithOrderItemsPagedAsync(Expression<Func<Order, bool>> condition,
        PagingParameters pagingParameters, bool trackChanges, CancellationToken cancellationToken);

    Task<Order> GetByIdWithOrderItemsAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}