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

        private void AddIsDeletedConstraint(ref Expression<Func<Mouse, bool>> expression, ICollection<bool> values)
        {
            if (values is not null && values.Any())
                expression = expression.And(m => values.Contains(m.IsDeleted));
        }

        private void AddNameConstraint(ref Expression<Func<Mouse, bool>> expression, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            var value = name.Trim();
            expression = expression.And(m => m.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddManufacturerConstraint(ref Expression<Func<Mouse, bool>> expression, ICollection<string> manufacturers)
        {
            if (manufacturers is not null && manufacturers.Any())
                expression = expression.And(mouse => manufacturers.Any(manufacturer => mouse.Manufacturer.Equals(manufacturer)));
        }

        private void AddMinPriceConstraint(ref Expression<Func<Mouse, bool>> expression, decimal? price)
        {
            if (price is not null)
                expression = expression.And(m => m.Price >= price);
        }

        private void AddMaxPriceConstraint(ref Expression<Func<Mouse, bool>> expression, decimal? price)
        {
            if (price is not null)
                expression = expression.And(m => m.Price <= price);
        }

        private void AddCreatedDateStartConstraint(ref Expression<Func<Mouse, bool>> expression, DateTime? date)
        {
            if (date is not null)
                expression = expression.And(m => m.Created >= date);
        }

        private void AddCreatedDateEndConstraint(ref Expression<Func<Mouse, bool>> expression, DateTime? date)
        {
            if (date is not null)
                expression = expression.And(m => m.Created <= date);
        }

        private void AddConnectionTypeConstraint(ref Expression<Func<Mouse, bool>> expression, ICollection<string> connectionTypes)
        {
            if (connectionTypes is not null && connectionTypes.Any())
                expression = expression.And(m => connectionTypes.Any(ct => ct.Equals(m.ConnectionType)));
        }

        private void AddBacklightConstraint(ref Expression<Func<Mouse, bool>> expression, ICollection<string> backlights)
        {
            if (backlights is not null && backlights.Any())
                expression = expression.And(m => backlights.Any(b => b.Equals(m.Backlight)));
        }
    }
}