using eStore_Admin.Application.Requests.Keyboards.Commands.Add;
using eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Add;
using FluentValidation;

namespace eStore_Admin.Application.Validation.KeyboardSwitches
{
    public class AddKeyboardSwitchCommandValidator : AbstractValidator<AddKeyboardSwitchCommand>
    {
        public AddKeyboardSwitchCommandValidator()
        {
            RuleFor(x => x.KeyboardSwitch)
                .NotNull()
                .SetValidator(new KeyboardSwitchRequestValidator());
        }
    }
}