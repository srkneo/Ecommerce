﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public ProductBrand Brands { get; set; }
        public ProductType Types { get; set; }
        public string ImageFile { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

    }
}
