using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrand = brandCollection.Find(b => true).Any();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "brands.json");

            if (!checkBrand)
            {
                //var brandsData = File.ReadAllText(path);
                var brandsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null && brands.Any())
                {
                    foreach (var brand in brands)
                    {
                        brandCollection.InsertOneAsync(brand);
                    }
                }
            }
            else 
            {
                Console.WriteLine("Brands already exist in the collection.");
            }
        }
    }
}
