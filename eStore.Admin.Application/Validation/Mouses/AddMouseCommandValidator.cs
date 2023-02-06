using eStore.Admin.Application.Requests.Mouses.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Mouses;

public class AddMouseCommandValidator : AbstractValidator<AddMouseCommand>
{
    public AddMouseCommandValidator()
    {
        RuleFor(x => x.Mouse)
            .NotNull()
            .SetValidator(new MouseRequestValidator());
    }
}