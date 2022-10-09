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

        private void AddIsDeletedConstraint(ref Expression<Func<Customer, bool>> expression, ICollection<bool> values)
        {
            if (values is not null && values.Any())
                expression = expression.And(c => values.Contains(c.IsDeleted));
        }

        private void AddFirstNameConstraint(ref Expression<Func<Customer, bool>> expression, string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return;

            expression = expression.And(c => c.FirstName.Contains(firstName.Trim()));
        }

        private void AddLastNameConstraint(ref Expression<Func<Customer, bool>> expression, string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return;

            expression = expression.And(c => c.LastName.Contains(lastName.Trim()));
        }

        private void AddPostalCodeConstraint(ref Expression<Func<Customer, bool>> expression, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return;

            expression = expression.And(c => c.PostalCode.Contains(postalCode.Trim()));
        }


        private void AddEmailConstraint(ref Expression<Func<Customer, bool>> expression, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            expression = expression.And(c => c.Email.Contains(email.Trim()));
        }

        private void AddPhoneNumberConstraint(ref Expression<Func<Customer, bool>> expression, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return;

            expression = expression.And(c => c.PhoneNumber.Contains(phoneNumber.Trim()));
        }

        private void AddCountryConstraint(ref Expression<Func<Customer, bool>> expression, string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return;

            expression = expression.And(c => c.Country.Contains(country.Trim()));
        }

        private void AddCityConstraint(ref Expression<Func<Customer, bool>> expression, string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return;

            expression = expression.And(c => c.City.Contains(city.Trim()));
        }

        private void AddAddressConstraint(ref Expression<Func<Customer, bool>> expression, string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return;

            expression = expression.And(c => c.Address.Contains(address.Trim()));
        }
    }
}