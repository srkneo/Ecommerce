using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviour
{
    // / <summary>
    /// Validation behaviour for MediatR requests. <summary>
    /// this will run all fluent validation validators against the request
    /// </summary>

    public class ValidationBehaviour<Treqeust, Tresponse> : IPipelineBehavior<Treqeust, Tresponse>
    where Treqeust : IRequest<Tresponse>
    {
        private readonly IEnumerable<IValidator<Treqeust>> _validators;
        public ValidationBehaviour( IEnumerable<IValidator<Treqeust>> validators )
        {
            _validators = validators;
        }

        public async Task<Tresponse> Handle(Treqeust request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()) 
            {
                var context = new ValidationContext<Treqeust>(request);

                //This will run all the validators against the request
                var validationResults = await Task.WhenAll(
                    _validators
                    .Select(v => v.ValidateAsync(context, cancellationToken))
                    );

                //If any of the validators have errors, throw an exception
                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            // Validate the request using the validators
            return await next();
        }
    }
}
