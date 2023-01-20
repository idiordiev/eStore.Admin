using eStore_Admin.Application.Requests.Mouses.Commands;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Mouses
{
    public class EditMouseCommandValidator : AbstractValidator<EditMouseCommand>
    {
        public EditMouseCommandValidator()
        {
            RuleFor(x => x.Mouse)
                .NotNull()
                .SetValidator(new MouseRequestValidator());
        }
    }
}