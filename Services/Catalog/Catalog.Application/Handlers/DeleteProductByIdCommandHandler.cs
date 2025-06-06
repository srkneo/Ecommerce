using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            return _productRepository.DeleteProduct(request.Id);
        }
    }
}
