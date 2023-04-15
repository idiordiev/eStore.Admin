﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Factories;

public class KeyboardPredicateFactory : IPredicateFactory<Keyboard, KeyboardFilterModel>
{
    public Expression<Func<Keyboard, bool>> CreateExpression(KeyboardFilterModel filterModel)
    {
        var expression = PredicateBuilder.True<Keyboard>();

        AddIsDeletedConstraint(ref expression, filterModel.IsDeletedValues);
        AddNameConstraint(ref expression, filterModel.Name);
        AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
        AddMinPriceConstraint(ref expression, filterModel.MinPrice);
        AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
        AddCreatedDateStartConstraint(ref expression, filterModel.CreatedStartDate);
        AddCreatedDateEndConstraint(ref expression, filterModel.CreatedEndDate);
        AddConnectionTypeConstraint(ref expression, filterModel.ConnectionTypes);
        AddTypeConstraint(ref expression, filterModel.Types);
        AddSizeConstraint(ref expression, filterModel.Sizes);
        AddSwitchConstraint(ref expression, filterModel.SwitchIds);
        AddKeyRolloverConstraint(ref expression, filterModel.KeyRollovers);
        AddBacklightConstraint(ref expression, filterModel.Backlights);

        return expression;
    }

    private static void AddIsDeletedConstraint(ref Expression<Func<Keyboard, bool>> expression, ICollection<bool> values)
    {
        if (values is not null && values.Any())
        {
            expression = expression.And(k => values.Contains(k.IsDeleted));
        }
    }

    private static void AddNameConstraint(ref Expression<Func<Keyboard, bool>> expression, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var value = name.Trim();
        expression = expression.And(k => k.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private static void AddManufacturerConstraint(ref Expression<Func<Keyboard, bool>> expression,
        ICollection<string> manufacturers)
    {
        if (manufacturers is not null && manufacturers.Any())
        {
            expression = expression.And(k =>
                manufacturers.Any(m => m.Equals(k.Manufacturer, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddMinPriceConstraint(ref Expression<Func<Keyboard, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(k => k.Price >= price);
        }
    }

    private static void AddMaxPriceConstraint(ref Expression<Func<Keyboard, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(k => k.Price <= price);
        }
    }

    private static void AddCreatedDateStartConstraint(ref Expression<Func<Keyboard, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(k => k.Created >= date);
        }
    }

    private static void AddCreatedDateEndConstraint(ref Expression<Func<Keyboard, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(k => k.Created <= date);
        }
    }

    private static void AddConnectionTypeConstraint(ref Expression<Func<Keyboard, bool>> expression,
        ICollection<string> connectionTypes)
    {
        if (connectionTypes is not null && connectionTypes.Any())
        {
            expression = expression.And(k =>
                connectionTypes.Any(ct =>
                    ct.Equals(k.ConnectionType, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddTypeConstraint(ref Expression<Func<Keyboard, bool>> expression, ICollection<string> types)
    {
        if (types is not null && types.Any())
        {
            expression = expression.And(k =>
                types.Any(t => t.Equals(k.Type, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddSizeConstraint(ref Expression<Func<Keyboard, bool>> expression, ICollection<string> sizes)
    {
        if (sizes is not null && sizes.Any())
        {
            expression = expression.And(k =>
                sizes.Any(s => s.Equals(k.Size, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddSwitchConstraint(ref Expression<Func<Keyboard, bool>> expression, ICollection<int?> switchIds)
    {
        if (switchIds is not null && switchIds.Any())
        {
            expression = expression.And(k => switchIds.Contains(k.SwitchId));
        }
    }

    private static void AddKeyRolloverConstraint(ref Expression<Func<Keyboard, bool>> expression,
        ICollection<string> keyRollovers)
    {
        if (keyRollovers is not null && keyRollovers.Any())
        {
            expression = expression.And(k =>
                keyRollovers.Any(kr => kr.Equals(k.KeyRollover, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddBacklightConstraint(ref Expression<Func<Keyboard, bool>> expression,
        ICollection<string> backlights)
    {
        if (backlights is not null && backlights.Any())
        {
            expression = expression.And(k =>
                backlights.Any(b => b.Equals(k.Backlight, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}