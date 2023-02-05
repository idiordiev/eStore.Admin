using eStore_Admin.Application.Requests.Mousepads.Commands;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Mousepads;

public class EditMousepadCommandValidator : AbstractValidator<EditMousepadCommand>
{
    public EditMousepadCommandValidator()
    {
        RuleFor(x => x.Mousepad)
            .NotNull()
            .SetValidator(new MousepadRequestValidator());
    }
}