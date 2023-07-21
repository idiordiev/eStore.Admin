using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Models;

public class MouseFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string Name { get; set; }
    public ICollection<string> Manufacturers { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime CreatedStartDate { get; set; }
    public DateTime CreatedEndDate { get; set; }
    public float? MinWeight { get; set; }
    public float? MaxWeight { get; set; }
    public ICollection<string> ConnectionTypes { get; set; }
    public ICollection<string> Backlights { get; set; }
    
    public Expression<Func<Mouse, bool>> CreateExpression()
    {
        var expression = PredicateBuilder.True<Mouse>();

        if (IsDeletedValues is not null && IsDeletedValues.Any())
        {
            expression = expression.And(m => IsDeletedValues.Contains(m.IsDeleted));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            expression = expression.And(m => m.Name.Equals(Name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        if (Manufacturers is not null && Manufacturers.Any())
        {
            expression = expression.And(mouse =>
                Manufacturers.Any(manufacturer => mouse.Manufacturer.Equals(manufacturer)));
        }

        if (MinPrice is not null)
        {
            expression = expression.And(m => m.Price >= MinPrice);
        }

        if (MaxPrice is not null)
        {
            expression = expression.And(m => m.Price <= MaxPrice);
        }

        if (CreatedStartDate != default)
        {
            expression = expression.And(m => m.Created >= CreatedStartDate);
        }

        if (CreatedEndDate != default)
        {
            expression = expression.And(m => m.Created <= CreatedEndDate);
        }

        if (ConnectionTypes is not null && ConnectionTypes.Any())
        {
            expression = expression.And(m => ConnectionTypes.Any(ct => ct.Equals(m.ConnectionType)));
        }

        if (Backlights is not null && Backlights.Any())
        {
            expression = expression.And(m => Backlights.Any(b => b.Equals(m.Backlight)));
        }

        return expression;
    }
}