using eStore_Admin.Application.Requests.Customers.Commands;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Customers;

public class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
{
    public EditCustomerCommandValidator()
    {
        RuleFor(x => x.Customer)
            .NotNull()
            .SetValidator(new CustomerRequestValidator());
    }
}