using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {
            bool checkType = typeCollection.Find(t => true).Any();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "types.json");
            if (!checkType)
            {
                var typesData = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null && types.Any())
                {
                    foreach (var type in types)
                    {
                        typeCollection.InsertOneAsync(type);
                    }
                }
            }
        }
    }
}
