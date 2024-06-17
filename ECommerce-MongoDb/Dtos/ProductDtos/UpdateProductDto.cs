using ECommerce_MongoDb.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce_MongoDb.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile? ImageFile{ get; set; }
    }
}
