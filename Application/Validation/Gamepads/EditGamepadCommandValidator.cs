using eStore_Admin.Application.Requests.Gamepads.Commands;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Gamepads
{
    public class EditGamepadCommandValidator : AbstractValidator<EditGamepadCommand>
    {
        public EditGamepadCommandValidator()
        {
            RuleFor(x => x.Gamepad)
                .NotNull()
                .SetValidator(new GamepadRequestValidator());
        }
    }
}