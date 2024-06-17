using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce_MongoDb.Entities
{
    public class Order
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        [BsonIgnore]
        public Customer Customer { get; set; }
        public string ProductId { get; set; }
        [BsonIgnore]
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
