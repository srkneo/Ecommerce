using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        async Task<bool> IRequestHandler<UpdateProductCommand, bool>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository.UpdateProduct(new Core.Entities.Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Summary = request.Summary,
                Brands = request.Brands,
                Types = request.Types,
                ImageFile = request.ImageFile,
                Price = request.Price
            });

            return true;
        }
    }
}
