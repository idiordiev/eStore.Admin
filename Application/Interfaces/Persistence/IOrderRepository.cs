using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence.Shared;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Interfaces.Persistence
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetAllWithOrderItemsPagedAsync(PagingParameters pagingParameters, bool trackChanges,
            CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetByConditionWithOrderItemsPagedAsync(Expression<Func<Order, bool>> condition,
            PagingParameters pagingParameters, bool trackChanges, CancellationToken cancellationToken);

        Task<Order> GetByIdWithOrderItemsAsync(int id, bool trackChanges, CancellationToken cancellationToken);
    }
}