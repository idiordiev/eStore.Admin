using eStore_Admin.Application.RequestModels;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Keyboards
{
    public class KeyboardRequestValidator : AbstractValidator<KeyboardRequest>
    {
        public KeyboardRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Manufacturer)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Size)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.KeycapMaterial)
                .MaximumLength(100);
            RuleFor(x => x.FrameMaterial)
                .MaximumLength(50);
            RuleFor(x => x.KeyRollover)
                .MaximumLength(20);
            RuleFor(x => x.Backlight)
                .MaximumLength(50);
            RuleFor(x => x.ConnectionType)
                .MaximumLength(50);
        }
    }
}