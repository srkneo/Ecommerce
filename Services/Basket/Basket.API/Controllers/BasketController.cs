using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Common;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var basket = await _mediator.Send(new GetBasketByUserNameQuery(userName));

            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdteBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var basket = await _mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            return Ok( await _mediator.Send(new DeleteBasketByUserNameCommand(userName)));
          
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // Get the basket by username
            var basket = await _mediator.Send(new GetBasketByUserNameQuery(basketCheckout.UserName));
            if (basket == null)
            {
                return BadRequest("Basket not found.");
            }
            var eventMsg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
            await _publishEndpoint.Publish(eventMsg);

            // Remove the basket after checkout
            await _mediator.Send(new DeleteBasketByUserNameCommand(basketCheckout.UserName));

            return Accepted();
        }

    }

}
