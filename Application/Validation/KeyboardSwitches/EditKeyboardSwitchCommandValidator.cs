using eStore_Admin.Application.Requests.KeyboardSwitches.Commands.Edit;
using FluentValidation;

namespace eStore_Admin.Application.Validation.KeyboardSwitches
{
    public class EditKeyboardSwitchCommandValidator : AbstractValidator<EditKeyboardSwitchCommand>
    {
        public EditKeyboardSwitchCommandValidator()
        {
            RuleFor(x => x.KeyboardSwitch)
                .NotNull()
                .SetValidator(new KeyboardSwitchRequestValidator());
        }
    }
}