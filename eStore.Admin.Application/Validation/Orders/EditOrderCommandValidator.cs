using eStore.Admin.Application.Requests.Orders.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Orders;

public class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
{
    public EditOrderCommandValidator()
    {
        RuleFor(x => x.Order)
            .NotNull()
            .SetValidator(new OrderRequestValidator());
    }
}