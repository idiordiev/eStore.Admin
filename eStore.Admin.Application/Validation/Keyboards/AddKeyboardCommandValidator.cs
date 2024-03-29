﻿using eStore.Admin.Application.Requests.Keyboards.Commands;
using FluentValidation;

namespace eStore.Admin.Application.Validation.Keyboards;

public class AddKeyboardCommandValidator : AbstractValidator<AddKeyboardCommand>
{
    public AddKeyboardCommandValidator()
    {
        RuleFor(x => x.Keyboard)
            .NotNull()
            .SetValidator(new KeyboardRequestValidator());
    }
}