using eStore_Admin.Application.Requests.OrderItems.Commands;
using FluentValidation;

namespace eStore_Admin.Application.Validation.OrderItems
{
    public class EditOrderItemCommandValidator : AbstractValidator<EditOrderItemCommand>
    {
        public EditOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderItem)
                .SetValidator(new OrderItemRequestValidator());
        }
    }
}