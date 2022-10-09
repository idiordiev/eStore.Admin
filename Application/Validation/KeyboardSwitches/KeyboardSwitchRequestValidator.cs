using eStore_Admin.Application.RequestModels;
using FluentValidation;

namespace eStore_Admin.Application.Validation.KeyboardSwitches
{
    public class KeyboardSwitchRequestValidator : AbstractValidator<KeyboardSwitchRequest>
    {
        public KeyboardSwitchRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Manufacturer)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}