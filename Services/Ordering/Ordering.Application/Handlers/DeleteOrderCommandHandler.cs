using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {

            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToDelete == null)
            {
                _logger.LogError($"Order with id {request.Id} not found.");
                throw new KeyNotFoundException($"Order with id {request.Id} not found.");
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order with id {request.Id} deleted successfully.");
            return Unit.Value;
        }
    }
}
