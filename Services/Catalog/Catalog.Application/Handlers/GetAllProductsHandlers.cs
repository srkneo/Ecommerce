using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandlers : IRequestHandler<GetAllProductQuery, Pagination<ProductResponse>>
    {

        private readonly IProductRepository _productRepository;

        public GetAllProductsHandlers(IProductRepository productRepository)
        {                
            _productRepository = productRepository;
        }

        public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {

            var products = await _productRepository.GetProducts(request.CatalogSpecParams);

            var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(products);
            return productResponseList;

        }
    }
}
