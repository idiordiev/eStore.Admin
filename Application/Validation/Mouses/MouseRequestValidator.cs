using eStore_Admin.Application.RequestModels;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Mouses
{
    public class MouseRequestValidator : AbstractValidator<MouseRequest>
    {
        public MouseRequestValidator()
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
            RuleFor(x => x.Backlight)
                .MaximumLength(50);
            RuleFor(x => x.ConnectionType)
                .MaximumLength(50);
            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.MinSensorDPI)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.MaxSensorDPI)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.ButtonsQuantity)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Length)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Width)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Height)
                .GreaterThanOrEqualTo(0);
        }
    }
}