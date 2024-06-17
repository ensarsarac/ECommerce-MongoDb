using ECommerce_MongoDb.Dtos.OrderDtos;

namespace ECommerce_MongoDb.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<ResultOrderDto>> GetAllOrderAsync();
        Task<GetByIdOrderDto> GetByIdOrderAsync(string id);
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task RemoveOrderAsync(string id);
        Task DescreateProductAmount(string id);
        Task IncreaseProductAmount(string id);
    }
}
