using eStore.Admin.Application.Requests.Gamepads.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Gamepads;

public class EditGamepadCommandValidator : AbstractValidator<EditGamepadCommand>
{
    public EditGamepadCommandValidator()
    {
        RuleFor(x => x.Gamepad)
            .NotNull()
            .SetValidator(new GamepadRequestValidator());
    }
}