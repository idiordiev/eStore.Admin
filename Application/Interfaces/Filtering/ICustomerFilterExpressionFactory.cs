using System;
using System.Linq.Expressions;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Interfaces.Filtering
{
    public interface ICustomerFilterExpressionFactory
    {
        Expression<Func<Customer, bool>> CreateExpression(CustomerFilterModel filterModel);
    }
}