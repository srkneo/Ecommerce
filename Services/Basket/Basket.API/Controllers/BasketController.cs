using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IMediator mediator, DiscountGrpcService discountGrpcService)
        {
            _mediator = mediator;
            _discountGrpcService = discountGrpcService;
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
            foreach (var item in createShoppingCartCommand.Items)
            {
                // Fetch discount for each item and apply it
                var discount = await _discountGrpcService.GetDiscount(item.ProductName);
                if (discount != null && discount.Amount > 0)
                {
                    item.Price -= discount.Amount;
                }
            }

            var basket = await _mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            return Ok( await _mediator.Send(new DeleteBasketByUserNameCommand(userName)));
          
        }


    }

}
