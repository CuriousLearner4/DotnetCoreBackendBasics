using Application.Exceptions;
using Application.PipelineBehaviour.Contracts;
using FluentValidation;
using MediatR;

namespace Application.PipelineBehaviour
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, IValidatable
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                List<String> errors = new();
                var validationResults = await Task
                    .WhenAll(
                    _validators
                        .Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f=>f!=null).ToList();
                if (failures.Count != 0)
                {
                    foreach(var failure in failures)
                    {
                        errors.Add(failure.ErrorMessage);
                    }
                    throw new CustomValidationException(errors,"one or more validation failures");
                }
            }
            return await next();
        }
    }
}
