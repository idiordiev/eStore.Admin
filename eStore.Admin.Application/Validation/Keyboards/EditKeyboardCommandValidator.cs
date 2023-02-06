using eStore.Admin.Application.Requests.Keyboards.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Keyboards;

public class EditKeyboardCommandValidator : AbstractValidator<EditKeyboardCommand>
{
    public EditKeyboardCommandValidator()
    {
        RuleFor(x => x.Keyboard)
            .NotNull()
            .SetValidator(new KeyboardRequestValidator());
    }
}