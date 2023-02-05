using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Infrastructure.Persistence.Repositories;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationContext context) : base(context)
    {
    }
}