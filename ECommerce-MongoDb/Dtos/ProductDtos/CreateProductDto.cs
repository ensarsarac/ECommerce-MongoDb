namespace ECommerce_MongoDb.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
