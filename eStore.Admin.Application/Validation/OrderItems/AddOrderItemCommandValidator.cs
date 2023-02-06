using eStore.Admin.Application.Requests.OrderItems.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.OrderItems;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderItem)
            .SetValidator(new OrderItemRequestValidator());
    }
}