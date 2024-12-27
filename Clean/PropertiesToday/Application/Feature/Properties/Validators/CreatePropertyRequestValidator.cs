
using Application.Feature.Properties.Commands;
using FluentValidation;

namespace Application.Feature.Properties.Validators
{
    public class CreatePropertyRequestValidator :AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropertyRequestValidator()
        {
            RuleFor(request => request.PropertyRequest).SetValidator(new NewPropertyValidator());
        }
    }
}
