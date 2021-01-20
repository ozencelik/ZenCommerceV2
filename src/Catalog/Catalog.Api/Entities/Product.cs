using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities
{
    public class Product
    {
        /// <summary>
        /// MongoDB use string BSON Id.
        /// That's why I'm creating a string Id object.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }
    }
}
