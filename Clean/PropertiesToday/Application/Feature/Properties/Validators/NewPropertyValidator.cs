﻿using Application.Models;
using FluentValidation;

namespace Application.Feature.Properties.Validators
{
    public class NewPropertyValidator :AbstractValidator<NewProperty>
    {
        public NewPropertyValidator()
        {
            RuleFor(np => np.Name).NotEmpty().WithMessage("Name is Requiered.").MaximumLength(15).WithMessage("Name should not exceed 15 characters.");
        }
    }
}
