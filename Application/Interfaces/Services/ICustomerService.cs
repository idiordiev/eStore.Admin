using System.Collections.Generic;
using System.Threading.Tasks;
using eStore_Admin.Application.DataTransferObjects;
using eStore_Admin.Application.FilterModels;

namespace eStore_Admin.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<IEnumerable<CustomerDto>> GetCustomersByFilterAsync(CustomerFilterModel filter);
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(CustomerDto customer);
        Task UpdateCustomerAsync(CustomerDto customer);
        Task DeleteCustomerAsync(int customerId);
    }
}