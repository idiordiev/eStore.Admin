using eStore_Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore_Admin.Application.Validation.OrderItems
{
    public class OrderItemRequestValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1);
        }
    }
}