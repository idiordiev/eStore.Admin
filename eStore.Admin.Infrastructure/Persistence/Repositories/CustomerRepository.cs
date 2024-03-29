﻿using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Infrastructure.Persistence.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationContext context) : base(context)
    {
    }
}