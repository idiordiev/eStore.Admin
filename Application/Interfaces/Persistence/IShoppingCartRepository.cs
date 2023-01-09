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
    public interface IShoppingCartRepository : IRepositoryBase<ShoppingCart>
    {
        Task<IEnumerable<ShoppingCart>> GetAllWithItemsPagedAsync(PagingParameters pagingParameters, bool trackChanges,
            CancellationToken cancellationToken);

        Task<IEnumerable<ShoppingCart>> GetByConditionWithItemsPagedAsync(
            Expression<Func<ShoppingCart, bool>> condition,
            PagingParameters pagingParameters, bool trackChanges, CancellationToken cancellationToken);

        Task<ShoppingCart> GetByIdWithItemsAsync(int id, bool trackChanges, CancellationToken cancellationToken);
    }
}