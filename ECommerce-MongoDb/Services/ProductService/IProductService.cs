using ECommerce_MongoDb.Dtos.ProductDtos;

namespace ECommerce_MongoDb.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task RemoveProductAsync(string id);
    }
}
