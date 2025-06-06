using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandHandler
    {
        private readonly IProductRepository _productRepository;
        public GetProductByBrandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByBrand(request.BrandName);
            var productsResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(products);

            return productsResponseList;
        }
    }
}
