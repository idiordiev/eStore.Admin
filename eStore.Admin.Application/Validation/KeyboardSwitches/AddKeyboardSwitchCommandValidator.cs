using eStore.Admin.Application.Requests.KeyboardSwitches.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.KeyboardSwitches;

public class AddKeyboardSwitchCommandValidator : AbstractValidator<AddKeyboardSwitchCommand>
{
    public AddKeyboardSwitchCommandValidator()
    {
        RuleFor(x => x.KeyboardSwitch)
            .NotNull()
            .SetValidator(new KeyboardSwitchRequestValidator());
    }
}