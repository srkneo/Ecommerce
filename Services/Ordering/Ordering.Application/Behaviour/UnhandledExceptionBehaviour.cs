using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviour
{
    public class UnhandledExceptionBehaviour<Trequest, Tresponse> : IPipelineBehavior<Trequest, Tresponse>
    where Trequest : IRequest<Tresponse>
    {
        private readonly ILogger<Trequest> _logger;
        public UnhandledExceptionBehaviour(ILogger<Trequest> logger)
        {
            _logger = logger;
        }

        public async Task<Tresponse> Handle(Trequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(Trequest).Name;
                _logger.LogError(ex, "Unhandled exception occured with Request: {Name} {@Request}", requestName, request);
                throw; // Re-throw the exception to ensure it propagates
            }
        }
    }
}
