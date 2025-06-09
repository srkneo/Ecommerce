

using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
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


        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products
                .Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams)
        {
            var biulder = Builders<Product>.Filter;
            var filter = biulder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = biulder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                filter &= biulder.Eq(p => p.Brands.Id, catalogSpecParams.BrandId);
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                filter &= biulder.Eq(p => p.Types.Id, catalogSpecParams.TypeId);
            }

            var totalItems = await _context.Products.CountDocumentsAsync(filter);
            var products = await _context.Products
                .Find(filter)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();
            
            return new Pagination<Product>(  catalogSpecParams.PageIndex,
                (int)totalItems, 
                catalogSpecParams.PageSize, products);
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
        {
            return await _context.Products
                .Find(p => p.Brands.Name.ToLower() == brandName.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _context.Products
                .Find(p => p.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct = await _context.Products
                .DeleteOneAsync(p => p.Id == id);
            return deletedProduct.DeletedCount > 0 && deletedProduct.IsAcknowledged;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await _context.Products
                .ReplaceOneAsync(p => p.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context.Brands
                .Find(b => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context.Types
                .Find(t => true).ToListAsync();
        }

    }
}
