namespace ECommerce_MongoDb.Dtos.OrderDtos
{
    public class ResultOrderDto
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerNameSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice{ get; set; }
        public int ProductStock{ get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
