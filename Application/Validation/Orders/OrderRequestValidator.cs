using eStore_Admin.Application.RequestModels;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Orders
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
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
}