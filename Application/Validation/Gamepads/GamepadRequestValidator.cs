using eStore_Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore_Admin.Application.Validation.Gamepads;

public class GamepadRequestValidator : AbstractValidator<GamepadDto>
{
    public GamepadRequestValidator()
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
        RuleFor(x => x.ConnectionType)
            .MaximumLength(50);
        RuleFor(x => x.Feedback)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Weight)
            .GreaterThanOrEqualTo(0);
    }
}