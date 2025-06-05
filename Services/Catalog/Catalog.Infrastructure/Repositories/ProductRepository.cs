

using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {

        public ICatalogContext _context { get; }

        public ProductRepository(ICatalogContext context)
        {
              _context = context;
        }


        async Task<Product> IProductRepository.GetProduct(string id)
        {
            return await _context.Products
                .Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProducts()
        {
            return await _context.Products
                .Find(p => true).ToListAsync();
        }

        Task<IEnumerable<Product>> IProductRepository.GetProductsByBrand(string name)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Product>> IProductRepository.GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        Task IProductRepository.CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        Task IProductRepository.DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }
        Task IProductRepository.UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        Task<IEnumerable<ProductBrand>> IBrandRepository.GetAllBrands()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProductType>> ITypesRepository.GetAllTypes()
        {
            throw new NotImplementedException();
        }

    }
}
