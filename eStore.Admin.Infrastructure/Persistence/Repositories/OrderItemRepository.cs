using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(ApplicationContext context) : base(context)
    {
    }
}