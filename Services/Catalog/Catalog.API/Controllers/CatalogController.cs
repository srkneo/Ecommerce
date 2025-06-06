using Catalog.Application.Commands;
using Catalog.Application.Handlers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> GetPorductById(string id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductByProductName(string productName)
        {
            var result = await _mediator.Send(new GetProductByNameQuery(productName));
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BrandResponse>> GetAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TypesResponse>> GetAllTypes()
        {
            var result = await _mediator.Send(new GetAllTypesQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductByBrand")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductByBrand(string brand)
        {
            var result = await _mediator.Send(new GetProductByBrandQuery(brand));
            return Ok(result);
        }

        [HttpPut]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send<ProductResponse>(command);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}",Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(string id)
        {
            var command = new DeleteProductByIdCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}


