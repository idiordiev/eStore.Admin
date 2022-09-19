using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<IOrderItemDto>> GetOrderItemsOfOrderAsync(int orderId);
        Task<IOrderItemDto> GetOrderItemByIdAsync(int orderItemId);
        Task AddOrderItemToOrderAsync(int orderId, IOrderItemDto orderItem);
        Task UpdateOrderItemAsync(int orderItemId, IOrderItemDto orderItemDto);
        Task DeleteOrderItemAsync(int orderItemId);
    }
}