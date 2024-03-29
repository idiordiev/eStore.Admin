﻿using eStore.Admin.Application.Requests.Mousepads.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Mousepads;

public class AddMousepadCommandValidator : AbstractValidator<AddMousepadCommand>
{
    public AddMousepadCommandValidator()
    {
        RuleFor(x => x.Mousepad)
            .NotNull()
            .SetValidator(new MousepadRequestValidator());
    }
}