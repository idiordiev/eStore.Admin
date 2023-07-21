using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Models;

public class CustomerFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    
    public Expression<Func<Customer, bool>> CreateExpression()
    {
        var expression = PredicateBuilder.True<Customer>();

        if (IsDeletedValues is not null && IsDeletedValues.Any())
        {
            expression = expression.And(c => IsDeletedValues.Contains(c.IsDeleted));
        }

        if (!string.IsNullOrWhiteSpace(FirstName))
        {
            expression = expression.And(c => c.FirstName.Contains(FirstName.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(LastName))
        {
            expression = expression.And(c => c.LastName.Contains(LastName.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(Email))
        {
            expression = expression.And(c => c.Email.Contains(Email.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(PhoneNumber))
        {
            expression = expression.And(c => c.PhoneNumber.Contains(PhoneNumber.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(Country))
        {
            expression = expression.And(c => c.Country.Contains(Country.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(City))
        {
            expression = expression.And(c => c.City.Contains(City.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(Address))
        {
            expression = expression.And(c => c.Address.Contains(Address.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(PostalCode))
        {
            expression = expression.And(c => c.PostalCode.Contains(PostalCode.Trim()));
        }

        return expression;
    }
}