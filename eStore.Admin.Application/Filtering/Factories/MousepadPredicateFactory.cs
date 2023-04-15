using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Factories;

public class MousepadPredicateFactory : IPredicateFactory<Mousepad, MousepadFilterModel>
{
    public Expression<Func<Mousepad, bool>> CreateExpression(MousepadFilterModel filterModel)
    {
        var expression = PredicateBuilder.True<Mousepad>();

        AddIsDeletedConstraint(ref expression, filterModel.IsDeletedValues);
        AddNameConstraint(ref expression, filterModel.Name);
        AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
        AddMinPriceConstraint(ref expression, filterModel.MinPrice);
        AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
        AddCreatedDateStartConstraint(ref expression, filterModel.CreatedStartDate);
        AddCreatedDateEndConstraint(ref expression, filterModel.CreatedEndDate);
        AddIsStitchedConstraint(ref expression, filterModel.IsStitchedValues);
        AddBottomMaterialConstraint(ref expression, filterModel.BottomMaterials);
        AddTopMaterialConstraint(ref expression, filterModel.TopMaterials);
        AddBacklightConstraint(ref expression, filterModel.Backlights);

        return expression;
    }

    private static void AddIsDeletedConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<bool> values)
    {
        if (values is not null && values.Any())
        {
            expression = expression.And(m => values.Contains(m.IsDeleted));
        }
    }

    private static void AddNameConstraint(ref Expression<Func<Mousepad, bool>> expression, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var value = name.Trim();
        expression = expression.And(m => m.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private static void AddManufacturerConstraint(ref Expression<Func<Mousepad, bool>> expression,
        ICollection<string> manufacturers)
    {
        if (manufacturers is not null && manufacturers.Any())
        {
            expression = expression.And(mouse =>
                manufacturers.Any(manufacturer => mouse.Manufacturer.Equals(manufacturer)));
        }
    }

    private static void AddMinPriceConstraint(ref Expression<Func<Mousepad, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(m => m.Price >= price);
        }
    }

    private static void AddMaxPriceConstraint(ref Expression<Func<Mousepad, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(m => m.Price <= price);
        }
    }

    private static void AddCreatedDateStartConstraint(ref Expression<Func<Mousepad, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(m => m.Created >= date);
        }
    }

    private static void AddCreatedDateEndConstraint(ref Expression<Func<Mousepad, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(m => m.Created <= date);
        }
    }

    private static void AddIsStitchedConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<bool> values)
    {
        if (values is not null && values.Any())
        {
            expression = expression.And(m => values.Contains(m.IsStitched));
        }
    }

    private static void AddBottomMaterialConstraint(ref Expression<Func<Mousepad, bool>> expression,
        ICollection<string> bottomMaterials)
    {
        if (bottomMaterials is not null && bottomMaterials.Any())
        {
            expression = expression.And(m => bottomMaterials.Any(b => b.Equals(m.BottomMaterial)));
        }
    }

    private static void AddTopMaterialConstraint(ref Expression<Func<Mousepad, bool>> expression,
        ICollection<string> topMaterials)
    {
        if (topMaterials is not null && topMaterials.Any())
        {
            expression = expression.And(m => topMaterials.Any(b => b.Equals(m.TopMaterial)));
        }
    }

    private static void AddBacklightConstraint(ref Expression<Func<Mousepad, bool>> expression, ICollection<string> backlights)
    {
        if (backlights is not null && backlights.Any())
        {
            expression = expression.And(m => backlights.Any(b => b.Equals(m.Backlight)));
        }
    }
}