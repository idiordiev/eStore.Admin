using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Models;

public class OrderFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public ICollection<int> CustomerIds { get; set; }
    public ICollection<int> StatusValues { get; set; }
    public decimal? MinTotal { get; set; }
    public decimal? MaxTotal { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    
    public Expression<Func<Order, bool>> CreateExpression()
    {
        var expression = PredicateBuilder.True<Order>();

        if (IsDeletedValues is not null && IsDeletedValues.Any())
        {
            expression = expression.And(o => IsDeletedValues.Contains(o.IsDeleted));
        }

        if (CustomerIds is not null && CustomerIds.Any())
        {
            expression = expression.And(o => CustomerIds.Contains(o.CustomerId));
        }

        if (StatusValues is not null && StatusValues.Any())
        {
            expression = expression.And(o => StatusValues.Contains((int)o.Status));
        }

        if (MinTotal is not null)
        {
            expression = expression.And(o => o.Total >= MinTotal);
        }

        if (MaxTotal is not null)
        {
            expression = expression.And(o => o.Total <= MaxTotal);
        }

        if (DateFrom != default)
        {
            expression = expression.And(m => m.TimeStamp >= DateFrom);
        }

        if (DateTo != default)
        {
            expression = expression.And(m => m.TimeStamp <= DateTo);
        }

        if (!string.IsNullOrWhiteSpace(Country))
        {
            expression = expression.And(c => c.ShippingCountry.Contains(Country.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(City))
        {
            expression = expression.And(c => c.ShippingCity.Contains(City.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(Address))
        {
            expression = expression.And(c => c.ShippingAddress.Contains(Address.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(PostalCode))
        {
            expression = expression.And(c => c.ShippingPostalCode.Contains(PostalCode.Trim()));
        }

        return expression;
    }
}