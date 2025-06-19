using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System.Collections.Specialized;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandHandlerV2 : IRequestHandler<CheckoutOrderCommandV2, int>
    {

        private readonly IOrderRepository _orderedRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;


        public CheckoutOrderCommandHandlerV2(IOrderRepository orderedRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {

            _orderedRepository = orderedRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommandV2 request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var genratedOrder = await _orderedRepository.AddAsync(orderEntity);
            _logger.LogInformation($"Order with id {genratedOrder.Id} is successfully created.");

            return genratedOrder.Id;
        }
    }
}
