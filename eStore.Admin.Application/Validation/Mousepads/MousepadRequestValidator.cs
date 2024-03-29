﻿using eStore.Admin.Application.RequestDTOs;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Mousepads;

public class MousepadRequestValidator : AbstractValidator<MousepadDto>
{
    public MousepadRequestValidator()
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
        RuleFor(x => x.TopMaterial)
            .MaximumLength(100);
        RuleFor(x => x.BottomMaterial)
            .MaximumLength(100);
        RuleFor(x => x.Length)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Width)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Height)
            .GreaterThanOrEqualTo(0);
    }
}