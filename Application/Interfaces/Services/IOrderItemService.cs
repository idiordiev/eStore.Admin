using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetOrderItemsOfOrderAsync(int orderId);
        Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId);
        Task AddOrderItemToOrderAsync(int orderId, OrderItemDto orderItem);
        Task UpdateOrderItemAsync(int orderItemId, OrderItemDto orderItemDto);
        Task DeleteOrderItemAsync(int orderItemId);
    }
}