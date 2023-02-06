using eStore.Admin.Application.Requests.KeyboardSwitches.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.KeyboardSwitches;

public class EditKeyboardSwitchCommandValidator : AbstractValidator<EditKeyboardSwitchCommand>
{
    public EditKeyboardSwitchCommandValidator()
    {
        RuleFor(x => x.KeyboardSwitch)
            .NotNull()
            .SetValidator(new KeyboardSwitchRequestValidator());
    }
}