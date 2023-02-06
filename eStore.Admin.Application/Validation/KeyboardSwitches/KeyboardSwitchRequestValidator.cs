using eStore.Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore.Admin.Application.Validation.KeyboardSwitches;

public class KeyboardSwitchRequestValidator : AbstractValidator<KeyboardSwitchDto>
{
    public KeyboardSwitchRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Manufacturer)
            .NotEmpty()
            .MaximumLength(150);
    }
}