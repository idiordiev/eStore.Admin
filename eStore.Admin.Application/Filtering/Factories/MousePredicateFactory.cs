using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Factories;

public class MousePredicateFactory : IPredicateFactory<Mouse, MouseFilterModel>
{
    public Expression<Func<Mouse, bool>> CreateExpression(MouseFilterModel filterModel)
    {
        var expression = PredicateBuilder.True<Mouse>();

        AddIsDeletedConstraint(ref expression, filterModel.IsDeletedValues);
        AddNameConstraint(ref expression, filterModel.Name);
        AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
        AddMinPriceConstraint(ref expression, filterModel.MinPrice);
        AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
        AddCreatedDateStartConstraint(ref expression, filterModel.CreatedStartDate);
        AddCreatedDateEndConstraint(ref expression, filterModel.CreatedEndDate);
        AddConnectionTypeConstraint(ref expression, filterModel.ConnectionTypes);
        AddBacklightConstraint(ref expression, filterModel.Backlights);

        return expression;
    }

    private static void AddIsDeletedConstraint(ref Expression<Func<Mouse, bool>> expression, ICollection<bool> values)
    {
        if (values is not null && values.Any())
        {
            expression = expression.And(m => values.Contains(m.IsDeleted));
        }
    }

    private static void AddNameConstraint(ref Expression<Func<Mouse, bool>> expression, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var value = name.Trim();
        expression = expression.And(m => m.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private static void AddManufacturerConstraint(ref Expression<Func<Mouse, bool>> expression,
        ICollection<string> manufacturers)
    {
        if (manufacturers is not null && manufacturers.Any())
        {
            expression = expression.And(mouse =>
                manufacturers.Any(manufacturer => mouse.Manufacturer.Equals(manufacturer)));
        }
    }

    private static void AddMinPriceConstraint(ref Expression<Func<Mouse, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(m => m.Price >= price);
        }
    }

    private static void AddMaxPriceConstraint(ref Expression<Func<Mouse, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(m => m.Price <= price);
        }
    }

    private static void AddCreatedDateStartConstraint(ref Expression<Func<Mouse, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(m => m.Created >= date);
        }
    }

    private static void AddCreatedDateEndConstraint(ref Expression<Func<Mouse, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(m => m.Created <= date);
        }
    }

    private static void AddConnectionTypeConstraint(ref Expression<Func<Mouse, bool>> expression,
        ICollection<string> connectionTypes)
    {
        if (connectionTypes is not null && connectionTypes.Any())
        {
            expression = expression.And(m => connectionTypes.Any(ct => ct.Equals(m.ConnectionType)));
        }
    }

    private static void AddBacklightConstraint(ref Expression<Func<Mouse, bool>> expression,
        ICollection<string> backlights)
    {
        if (backlights is not null && backlights.Any())
        {
            expression = expression.And(m => backlights.Any(b => b.Equals(m.Backlight)));
        }
    }
}