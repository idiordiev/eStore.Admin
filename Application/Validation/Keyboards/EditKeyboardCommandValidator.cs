using eStore_Admin.Application.Requests.Keyboards.Commands;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Keyboards
{
    public class EditKeyboardCommandValidator : AbstractValidator<EditKeyboardCommand>
    {
        public EditKeyboardCommandValidator()
        {
            RuleFor(x => x.Keyboard)
                .NotNull()
                .SetValidator(new KeyboardRequestValidator());
        }
    }
}