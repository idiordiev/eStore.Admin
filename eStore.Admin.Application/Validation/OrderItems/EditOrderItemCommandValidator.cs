using eStore.Admin.Application.Requests.OrderItems.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.OrderItems;

public class EditOrderItemCommandValidator : AbstractValidator<EditOrderItemCommand>
{
    public EditOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderItem)
            .SetValidator(new OrderItemRequestValidator());
    }
}