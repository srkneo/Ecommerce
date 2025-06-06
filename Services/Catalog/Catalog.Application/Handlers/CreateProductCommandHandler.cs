using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            if (productEntity is null) {
                throw new ArgumentNullException(nameof(productEntity), "Product entity cannot be null");
            }

            var createdProduct = await _productRepository.CreateProduct(productEntity);
            var ProductResponse = ProductMapper.Mapper.Map<ProductResponse>(createdProduct);
            return ProductResponse;

        }
    }
}
