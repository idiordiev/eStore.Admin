using eStore_Admin.Application.Requests.Keyboards.Commands.Add;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Keyboards
{
    public class AddKeyboardCommandValidator : AbstractValidator<AddKeyboardCommand>
    {
        public AddKeyboardCommandValidator()
        {
            RuleFor(x => x.Keyboard)
                .NotNull()
                .SetValidator(new KeyboardRequestValidator());
        }
    }
}