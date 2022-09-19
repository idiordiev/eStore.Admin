using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;
using eStore_Admin.Application.Interfaces.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<IOrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<IOrderDto>> GetOrdersByFilterAsync(IOrderFilterModel filter);
        Task<IOrderDto> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(IOrderDto order);
        Task UpdateOrderAsync(int orderId, IOrderDto order);
        Task DeleteOrderAsync(int orderId);
    }
}