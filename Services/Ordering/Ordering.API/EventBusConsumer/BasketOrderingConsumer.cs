﻿using AutoMapper;
using EventBus.Messages.Common;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketOrderingConsumer> _logger;

        public BasketOrderingConsumer(IMediator mediator,IMapper mapper,ILogger<BasketOrderingConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            using var scope = _logger.BeginScope("Consuming basket checkout event for {correlaitonId}", context.Message.CorrelationId);
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            await _mediator.Send(command);
            _logger.LogInformation("Basket checkout event consumed successfully for {correlaitonId}", context.Message.CorrelationId);
        }
    }
}
