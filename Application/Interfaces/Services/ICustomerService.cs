using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.DataTransferObjects;
using eStore_Admin.Application.Interfaces.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<ICustomerDto>> GetAllCustomersAsync();
        Task<IEnumerable<ICustomerDto>> GetCustomersByFilterAsync(ICustomerFilterModel filter);
        Task<ICustomerDto> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(ICustomerDto customer);
        Task UpdateCustomerAsync(ICustomerDto customer);
        Task DeleteCustomerAsync(int customerId);
    }
}