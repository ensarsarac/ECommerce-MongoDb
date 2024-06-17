using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce_MongoDb.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }
        public string CustomerNameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
