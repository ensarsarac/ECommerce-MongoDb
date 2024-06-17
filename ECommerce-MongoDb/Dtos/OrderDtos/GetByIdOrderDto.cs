using ECommerce_MongoDb.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce_MongoDb.Dtos.OrderDtos
{
    public class GetByIdOrderDto
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
