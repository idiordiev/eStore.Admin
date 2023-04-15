﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Factories;

public class GamepadPredicateFactory : IPredicateFactory<Gamepad, GamepadFilterModel>
{
    public Expression<Func<Gamepad, bool>> CreateExpression(GamepadFilterModel filterModel)
    {
        var expression = PredicateBuilder.True<Gamepad>();

        AddIsDeletedConstraint(ref expression, filterModel.IsDeletedValues);
        AddNameConstraint(ref expression, filterModel.Name);
        AddManufacturerConstraint(ref expression, filterModel.Manufacturers);
        AddMinPriceConstraint(ref expression, filterModel.MinPrice);
        AddMaxPriceConstraint(ref expression, filterModel.MaxPrice);
        AddCreatedDateStartConstraint(ref expression, filterModel.CreatedStartDate);
        AddCreatedDateEndConstraint(ref expression, filterModel.CreatedEndDate);
        AddConnectionTypeConstraint(ref expression, filterModel.ConnectionTypes);
        AddCompatibleDeviceConstraint(ref expression, filterModel.CompatibleDevices);
        AddFeedbackConstraint(ref expression, filterModel.Feedbacks);
        AddMinWeightConstraint(ref expression, filterModel.MinWeight);
        AddMaxWeightConstraint(ref expression, filterModel.MaxWeight);

        return expression;
    }

    private static void AddIsDeletedConstraint(ref Expression<Func<Gamepad, bool>> expression, ICollection<bool> values)
    {
        if (values is not null && values.Any())
        {
            expression = expression.And(g => values.Contains(g.IsDeleted));
        }
    }

    private static void AddNameConstraint(ref Expression<Func<Gamepad, bool>> expression, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var value = name.Trim();
        expression = expression.And(g => g.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private static void AddManufacturerConstraint(ref Expression<Func<Gamepad, bool>> expression,
        ICollection<string> manufacturers)
    {
        if (manufacturers is not null && manufacturers.Any())
        {
            expression = expression.And(g =>
                manufacturers.Any(m => m.Equals(g.Manufacturer, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddFeedbackConstraint(ref Expression<Func<Gamepad, bool>> expression,
        ICollection<string> feedbacks)
    {
        if (feedbacks is not null && feedbacks.Any())
        {
            expression = expression.And(g =>
                feedbacks.Any(f => f.Equals(g.Feedback, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddMinPriceConstraint(ref Expression<Func<Gamepad, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(g => g.Price >= price);
        }
    }

    private static void AddMaxPriceConstraint(ref Expression<Func<Gamepad, bool>> expression, decimal? price)
    {
        if (price is not null)
        {
            expression = expression.And(g => g.Price <= price);
        }
    }

    private static void AddCreatedDateStartConstraint(ref Expression<Func<Gamepad, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(g => g.Created >= date);
        }
    }

    private static void AddCreatedDateEndConstraint(ref Expression<Func<Gamepad, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(g => g.Created <= date);
        }
    }

    private static void AddConnectionTypeConstraint(ref Expression<Func<Gamepad, bool>> expression,
        ICollection<string> connectionTypes)
    {
        if (connectionTypes is not null && connectionTypes.Any())
        {
            expression = expression.And(g =>
                connectionTypes.Any(ct =>
                    ct.Equals(g.ConnectionType, StringComparison.InvariantCultureIgnoreCase)));
        }
    }

    private static void AddCompatibleDeviceConstraint(ref Expression<Func<Gamepad, bool>> expression,
        ICollection<string> compatibleDevices)
    {
        if (compatibleDevices is not null && compatibleDevices.Any())
        {
            expression = expression.And(g =>
                g.CompatibleDevices.Any(cd =>
                    compatibleDevices.Any(value => value.Equals(cd, StringComparison.InvariantCultureIgnoreCase))));
        }
    }

    private static void AddMinWeightConstraint(ref Expression<Func<Gamepad, bool>> expression, float? weight)
    {
        if (weight is not null)
        {
            expression = expression.And(g => g.Weight >= weight);
        }
    }

    private static void AddMaxWeightConstraint(ref Expression<Func<Gamepad, bool>> expression, float? weight)
    {
        if (weight is not null)
        {
            expression = expression.And(g => g.Weight <= weight);
        }
    }
}