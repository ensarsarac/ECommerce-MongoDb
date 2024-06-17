namespace ECommerce_MongoDb.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
