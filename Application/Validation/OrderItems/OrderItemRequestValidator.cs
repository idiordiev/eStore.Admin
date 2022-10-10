using eStore_Admin.Application.RequestModels;
using FluentValidation;

namespace eStore_Admin.Application.Validation.OrderItems
{
    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
    {
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1);
        }
    }
}