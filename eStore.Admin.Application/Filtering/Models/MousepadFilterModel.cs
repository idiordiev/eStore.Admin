using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Models;

public class MousepadFilterModel
{
    public ICollection<bool> IsDeletedValues { get; set; }
    public string Name { get; set; }
    public ICollection<string> Manufacturers { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime CreatedStartDate { get; set; }
    public DateTime CreatedEndDate { get; set; }
    public ICollection<bool> IsStitchedValues { get; set; }
    public ICollection<string> BottomMaterials { get; set; }
    public ICollection<string> TopMaterials { get; set; }
    public ICollection<string> Backlights { get; set; }
    
    public Expression<Func<Mousepad, bool>> CreateExpression()
    {
        var expression = PredicateBuilder.True<Mousepad>();

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

        if (IsStitchedValues is not null && IsStitchedValues.Any())
        {
            expression = expression.And(m => IsStitchedValues.Contains(m.IsStitched));
        }

        if (BottomMaterials is not null && BottomMaterials.Any())
        {
            expression = expression.And(m => BottomMaterials.Any(b => b.Equals(m.BottomMaterial)));
        }

        if (TopMaterials is not null && TopMaterials.Any())
        {
            expression = expression.And(m => TopMaterials.Any(b => b.Equals(m.TopMaterial)));
        }

        if (Backlights is not null && Backlights.Any())
        {
            expression = expression.And(m => Backlights.Any(b => b.Equals(m.Backlight)));
        }

        return expression;
    }
}