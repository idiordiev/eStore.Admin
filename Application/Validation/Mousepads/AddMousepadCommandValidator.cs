using eStore_Admin.Application.Requests.Mousepads.Commands.Add;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Mousepads
{
    public class AddMousepadCommandValidator : AbstractValidator<AddMousepadCommand>
    {
        public AddMousepadCommandValidator()
        {
            RuleFor(x => x.Mousepad)
                .NotNull()
                .SetValidator(new MousepadRequestValidator());
        }
    }
}