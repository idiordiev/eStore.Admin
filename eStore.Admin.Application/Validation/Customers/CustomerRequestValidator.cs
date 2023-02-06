using System.Text.RegularExpressions;
using eStore.Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Customers;

public class CustomerRequestValidator : AbstractValidator<CustomerDto>
{
    public CustomerRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(120);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(120);
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(100)
            .EmailAddress()
            .WithMessage("Email is not valid.");
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .Must(BeAValidPhoneNumber);
        RuleFor(x => x.Country)
            .MaximumLength(100);
        RuleFor(x => x.City)
            .MaximumLength(100);
        RuleFor(x => x.Address)
            .MaximumLength(100);
        RuleFor(x => x.PostalCode)
            .MaximumLength(10);
    }

    private bool BeAValidPhoneNumber(string arg)
    {
        var regex = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
        return regex.IsMatch(arg);
    }
}