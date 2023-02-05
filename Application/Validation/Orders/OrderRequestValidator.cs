using eStore_Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Orders;

public class OrderRequestValidator : AbstractValidator<OrderDto>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.ShippingCountry)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.ShippingCity)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.ShippingAddress)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.ShippingPostalCode)
            .NotEmpty()
            .MaximumLength(10);
    }
}