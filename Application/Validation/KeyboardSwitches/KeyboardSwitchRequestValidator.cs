using eStore_Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore_Admin.Application.Validation.KeyboardSwitches
{
    public class KeyboardSwitchRequestValidator : AbstractValidator<KeyboardSwitchDto>
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