using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Filtering.Factories
{
    public class CustomerFilterExpressionFactory : ICustomerFilterExpressionFactory
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

        private void AddIsDeletedConstraint(ref Expression<Func<Customer, bool>> expression, IEnumerable<bool> values)
        {
            var valuesArray = values.ToArray();
            if (valuesArray.Any())
                expression = expression.And(c => valuesArray.Contains(c.IsDeleted));
        }

        private void AddFirstNameConstraint(ref Expression<Func<Customer, bool>> expression, string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return;

            var value = firstName.Trim();
            expression = expression.And(c => c.FirstName.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddLastNameConstraint(ref Expression<Func<Customer, bool>> expression, string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return;

            var value = lastName.Trim();
            expression = expression.And(c => c.LastName.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddPostalCodeConstraint(ref Expression<Func<Customer, bool>> expression, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return;

            var value = postalCode.Trim();
            expression = expression.And(c => c.PostalCode.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }


        private void AddEmailConstraint(ref Expression<Func<Customer, bool>> expression, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            var value = email.Trim();
            expression = expression.And(c => c.Email.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddPhoneNumberConstraint(ref Expression<Func<Customer, bool>> expression, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return;

            var value = phoneNumber.Trim();
            expression = expression.And(c => c.PhoneNumber.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddCountryConstraint(ref Expression<Func<Customer, bool>> expression, string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return;

            var value = country.Trim();
            expression = expression.And(c => c.Country.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddCityConstraint(ref Expression<Func<Customer, bool>> expression, string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return;

            var value = city.Trim();
            expression = expression.And(c => c.City.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddAddressConstraint(ref Expression<Func<Customer, bool>> expression, string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return;

            var value = address.Trim();
            expression = expression.And(c => c.Address.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}