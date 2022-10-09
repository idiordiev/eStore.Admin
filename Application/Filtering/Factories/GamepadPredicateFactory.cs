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

        private void AddIsDeletedConstraint(ref Expression<Func<Gamepad, bool>> expression, IEnumerable<bool> values)
        {
            IEnumerable<bool> valuesArray = values as bool[] ?? values.ToArray();
            if (valuesArray.Any())
                expression = expression.And(g => valuesArray.Contains(g.IsDeleted));
        }

        private void AddNameConstraint(ref Expression<Func<Gamepad, bool>> expression, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            var value = name.Trim();
            expression = expression.And(g => g.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddManufacturerConstraint(ref Expression<Func<Gamepad, bool>> expression, IEnumerable<string> manufacturers)
        {
            IEnumerable<string> values = manufacturers as string[] ?? manufacturers.ToArray();
            if (values.Any())
                expression = expression.And(g => values.Any(m => m.Equals(g.Manufacturer, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void AddFeedbackConstraint(ref Expression<Func<Gamepad, bool>> expression, IEnumerable<string> feedbacks)
        {
            IEnumerable<string> values = feedbacks as string[] ?? feedbacks.ToArray();
            if (values.Any())
                expression = expression.And(g => values.Any(f => f.Equals(g.Feedback, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void AddMinPriceConstraint(ref Expression<Func<Gamepad, bool>> expression, decimal? price)
        {
            if (price is not null)
                expression = expression.And(g => g.Price >= price);
        }

        private void AddMaxPriceConstraint(ref Expression<Func<Gamepad, bool>> expression, decimal? price)
        {
            if (price is not null)
                expression = expression.And(g => g.Price <= price);
        }

        private void AddCreatedDateStartConstraint(ref Expression<Func<Gamepad, bool>> expression, DateTime? date)
        {
            if (date is not null)
                expression = expression.And(g => g.Created >= date);
        }

        private void AddCreatedDateEndConstraint(ref Expression<Func<Gamepad, bool>> expression, DateTime? date)
        {
            if (date is not null)
                expression = expression.And(g => g.Created <= date);
        }

        private void AddConnectionTypeConstraint(ref Expression<Func<Gamepad, bool>> expression, IEnumerable<string> connectionTypes)
        {
            IEnumerable<string> values = connectionTypes as string[] ?? connectionTypes.ToArray();
            if (values.Any())
                expression = expression.And(g =>
                    values.Any(ct => ct.Equals(g.ConnectionType, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void AddCompatibleDeviceConstraint(ref Expression<Func<Gamepad, bool>> expression, IEnumerable<string> compatibleDevices)
        {
            IEnumerable<string> values = compatibleDevices as string[] ?? compatibleDevices.ToArray();
            if (values.Any())
                expression = expression.And(g =>
                    g.CompatibleDevices.Any(cd => values.Any(value => value.Equals(cd, StringComparison.InvariantCultureIgnoreCase))));
        }

        private void AddMinWeightConstraint(ref Expression<Func<Gamepad, bool>> expression, float? weight)
        {
            if (weight is not null)
                expression = expression.And(g => g.Weight >= weight);
        }

        private void AddMaxWeightConstraint(ref Expression<Func<Gamepad, bool>> expression, float? weight)
        {
            if (weight is not null)
                expression = expression.And(g => g.Weight <= weight);
        }
    }
}