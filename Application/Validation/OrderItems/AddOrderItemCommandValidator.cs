using eStore_Admin.Application.Requests.OrderItems.Commands.Add;
using FluentValidation;

namespace eStore_Admin.Application.Validation.OrderItems
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderItem)
                .SetValidator(new OrderItemRequestValidator());
        }
    }
}