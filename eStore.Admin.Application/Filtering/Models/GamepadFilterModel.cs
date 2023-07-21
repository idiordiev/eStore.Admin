using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Models;

public class GamepadFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string Name { get; set; }
    public ICollection<string> Manufacturers { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? CreatedStartDate { get; set; }
    public DateTime? CreatedEndDate { get; set; }
    public ICollection<string> ConnectionTypes { get; set; }
    public ICollection<string> Feedbacks { get; set; }
    public ICollection<string> CompatibleDevices { get; set; }
    public float? MinWeight { get; set; }
    public float? MaxWeight { get; set; }
    
    public Expression<Func<Gamepad, bool>> CreateExpression()
    {
        var expression = PredicateBuilder.True<Gamepad>();

        if (IsDeletedValues is not null && IsDeletedValues.Any())
        {
            expression = expression.And(g => IsDeletedValues.Contains(g.IsDeleted));
        }

        if (!string.IsNullOrWhiteSpace(Name))
        {
            expression = expression.And(g => g.Name.Equals(Name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        if (Manufacturers is not null && Manufacturers.Any())
        {
            expression = expression.And(g =>
                Manufacturers.Any(m => m.Equals(g.Manufacturer, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (MinPrice is not null)
        {
            expression = expression.And(g => g.Price >= MinPrice);
        }

        if (MaxPrice is not null)
        {
            expression = expression.And(g => g.Price <= MaxPrice);
        }

        if (CreatedStartDate is not null)
        {
            expression = expression.And(g => g.Created >= CreatedStartDate);
        }

        if (CreatedEndDate is not null)
        {
            expression = expression.And(g => g.Created <= CreatedEndDate);
        }

        if (ConnectionTypes is not null && ConnectionTypes.Any())
        {
            expression = expression.And(g =>
                ConnectionTypes.Any(ct =>
                    ct.Equals(g.ConnectionType, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (CompatibleDevices is not null && CompatibleDevices.Any())
        {
            expression = expression.And(g =>
                g.CompatibleDevices.Any(cd =>
                    CompatibleDevices.Any(value => value.Equals(cd, StringComparison.InvariantCultureIgnoreCase))));
        }

        if (Feedbacks is not null && Feedbacks.Any())
        {
            expression = expression.And(g =>
                Feedbacks.Any(f => f.Equals(g.Feedback, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (MinWeight is not null)
        {
            expression = expression.And(g => g.Weight >= MinWeight);
        }

        if (MaxWeight is not null)
        {
            expression = expression.And(g => g.Weight <= MaxWeight);
        }

        return expression;
    }
}