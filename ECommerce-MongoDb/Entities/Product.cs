using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce_MongoDb.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
        public string StorageName { get; set; }
    }
}
