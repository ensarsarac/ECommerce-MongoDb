using ECommerce_MongoDb.Dtos.CustomerDtos;

namespace ECommerce_MongoDb.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDto>> GetAllCustomerAsync();
        Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id);
        Task CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
        Task RemoveCustomerAsync(string id);
    }
}
