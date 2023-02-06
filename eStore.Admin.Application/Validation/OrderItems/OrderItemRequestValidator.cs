using eStore.Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore.Admin.Application.Validation.OrderItems;

public class OrderItemRequestValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1);
    }
}