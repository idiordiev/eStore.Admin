﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore.Admin.Application.Filtering.Models;
using eStore.Admin.Application.Interfaces.Filtering;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Filtering.Factories;

public class CustomerPredicateFactory : IPredicateFactory<Customer, CustomerFilterModel>
{
    public Expression<Func<Customer, bool>> CreateExpression(CustomerFilterModel filterModel)
    {
        var expression = PredicateBuilder.True<Customer>();

        AddIsDeletedConstraint(ref expression, filterModel.IsDeletedValues);
        AddFirstNameConstraint(ref expression, filterModel.FirstName);
        AddLastNameConstraint(ref expression, filterModel.LastName);
        AddEmailConstraint(ref expression, filterModel.Email);
        AddPhoneNumberConstraint(ref expression, filterModel.PhoneNumber);
        AddCountryConstraint(ref expression, filterModel.Country);
        AddCityConstraint(ref expression, filterModel.City);
        AddAddressConstraint(ref expression, filterModel.Address);
        AddPostalCodeConstraint(ref expression, filterModel.PostalCode);

        return expression;
    }

    private static void AddIsDeletedConstraint(ref Expression<Func<Customer, bool>> expression,
        ICollection<bool> values)
    {
        if (values is not null && values.Any())
            expression = expression.And(c => values.Contains(c.IsDeleted));
    }

    private static void AddFirstNameConstraint(ref Expression<Func<Customer, bool>> expression, string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return;

        expression = expression.And(c => c.FirstName.Contains(firstName.Trim()));
    }

    private static void AddLastNameConstraint(ref Expression<Func<Customer, bool>> expression, string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return;

        expression = expression.And(c => c.LastName.Contains(lastName.Trim()));
    }

    private static void AddPostalCodeConstraint(ref Expression<Func<Customer, bool>> expression, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return;

        expression = expression.And(c => c.PostalCode.Contains(postalCode.Trim()));
    }


    private static void AddEmailConstraint(ref Expression<Func<Customer, bool>> expression, string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        expression = expression.And(c => c.Email.Contains(email.Trim()));
    }

    private static void AddPhoneNumberConstraint(ref Expression<Func<Customer, bool>> expression, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return;

        expression = expression.And(c => c.PhoneNumber.Contains(phoneNumber.Trim()));
    }

    private static void AddCountryConstraint(ref Expression<Func<Customer, bool>> expression, string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return;

        expression = expression.And(c => c.Country.Contains(country.Trim()));
    }

    private static  void AddCityConstraint(ref Expression<Func<Customer, bool>> expression, string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return;

        expression = expression.And(c => c.City.Contains(city.Trim()));
    }

    private static void AddAddressConstraint(ref Expression<Func<Customer, bool>> expression, string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return;

        expression = expression.And(c => c.Address.Contains(address.Trim()));
    }
}