using eStore_Admin.Application.Requests.Customers.Commands.Add;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Customers
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(x => x.Customer)
                .NotNull()
                .SetValidator(new CustomerRequestValidator());
        }
    }
}