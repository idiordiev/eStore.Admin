using eStore.Admin.Application.Requests.Gamepads.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Gamepads;

public class AddGamepadCommandValidator : AbstractValidator<AddGamepadCommand>
{
    public AddGamepadCommandValidator()
    {
        RuleFor(x => x.Gamepad)
            .NotNull()
            .SetValidator(new GamepadRequestValidator());
    }
}