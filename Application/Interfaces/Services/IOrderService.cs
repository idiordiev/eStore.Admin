using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;
using eStore_Admin.Application.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersByFilterAsync(OrderFilterModel filter);
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(OrderDto order);
        Task UpdateOrderAsync(int orderId, OrderDto order);
        Task DeleteOrderAsync(int orderId);
    }
}