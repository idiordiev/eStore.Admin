﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Factories;

public class OrderPredicateFactory : IPredicateFactory<Order, OrderFilterModel>
{
    public Expression<Func<Order, bool>> CreateExpression(OrderFilterModel filterModel)
    {
        var expression = PredicateBuilder.True<Order>();

        AddIsDeletedConstraint(ref expression, filterModel.IsDeletedValues);
        AddCustomerConstraint(ref expression, filterModel.CustomerIds);
        AddStatusConstraint(ref expression, filterModel.StatusValues);
        AddMinTotalConstraint(ref expression, filterModel.MinTotal);
        AddMaxTotalConstraint(ref expression, filterModel.MaxTotal);
        AddDateFromConstraint(ref expression, filterModel.DateFrom);
        AddDateToConstraint(ref expression, filterModel.DateTo);
        AddCountryConstraint(ref expression, filterModel.Country);
        AddCityConstraint(ref expression, filterModel.City);
        AddAddressConstraint(ref expression, filterModel.Address);
        AddPostalCodeConstraint(ref expression, filterModel.PostalCode);

        return expression;
    }

    private static void AddIsDeletedConstraint(ref Expression<Func<Order, bool>> expression, ICollection<bool> values)
    {
        if (values is not null && values.Any())
        {
            expression = expression.And(o => values.Contains(o.IsDeleted));
        }
    }

    private static void AddCustomerConstraint(ref Expression<Func<Order, bool>> expression, ICollection<int> customerIds)
    {
        if (customerIds is not null && customerIds.Any())
        {
            expression = expression.And(o => customerIds.Contains(o.CustomerId));
        }
    }

    private static void AddStatusConstraint(ref Expression<Func<Order, bool>> expression, ICollection<int> statusValues)
    {
        if (statusValues is not null && statusValues.Any())
        {
            expression = expression.And(o => statusValues.Contains((int)o.Status));
        }
    }

    private static void AddMinTotalConstraint(ref Expression<Func<Order, bool>> expression, decimal? total)
    {
        if (total is not null)
        {
            expression = expression.And(o => o.Total >= total);
        }
    }

    private static void AddMaxTotalConstraint(ref Expression<Func<Order, bool>> expression, decimal? total)
    {
        if (total is not null)
        {
            expression = expression.And(o => o.Total <= total);
        }
    }

    private static void AddDateFromConstraint(ref Expression<Func<Order, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(m => m.TimeStamp >= date);
        }
    }

    private static void AddDateToConstraint(ref Expression<Func<Order, bool>> expression, DateTime? date)
    {
        if (date is not null)
        {
            expression = expression.And(m => m.TimeStamp <= date);
        }
    }

    private static void AddCountryConstraint(ref Expression<Func<Order, bool>> expression, string country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return;
        }

        expression = expression.And(c => c.ShippingCountry.Contains(country.Trim()));
    }

    private static void AddCityConstraint(ref Expression<Func<Order, bool>> expression, string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return;
        }

        expression = expression.And(c => c.ShippingCity.Contains(city.Trim()));
    }

    private static void AddAddressConstraint(ref Expression<Func<Order, bool>> expression, string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return;
        }

        expression = expression.And(c => c.ShippingAddress.Contains(address.Trim()));
    }

    private static void AddPostalCodeConstraint(ref Expression<Func<Order, bool>> expression, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            return;
        }

        expression = expression.And(c => c.ShippingPostalCode.Contains(postalCode.Trim()));
    }
}