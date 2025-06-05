using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandlers : IRequestHandler<GetAllProductQuery, IList<ProductResponse>>
    {

        private readonly IProductRepository _productRepository;

        public GetAllProductsHandlers(IProductRepository productRepository)
        {
                
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {

            var products = await _productRepository.GetProducts();

            var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(products.ToList());
            return productResponseList;

        }
    }
}
