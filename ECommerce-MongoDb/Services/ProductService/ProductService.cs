using AutoMapper;
using ECommerce_MongoDb.Dtos.CategoryDtos;
using ECommerce_MongoDb.Dtos.ProductDtos;
using ECommerce_MongoDb.Entities;
using ECommerce_MongoDb.Services.GCSService;
using ECommerce_MongoDb.Settings;
using MongoDB.Driver;

namespace ECommerce_MongoDb.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IGCSService _gcsservice;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSetting _databaseSetting, IGCSService gcsservice)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSetting.ConnectionString);
            var database = client.GetDatabase(_databaseSetting.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSetting.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSetting.CategoryCollectionName);
            _gcsservice = gcsservice;
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            var storageName = Guid.NewGuid().ToString() + "-" + createProductDto.ImageUrl.FileName;
            value.ImageUrl = await _gcsservice.UploadFileAsync(createProductDto.ImageUrl,storageName);
            value.StorageName = storageName;
            await _productCollection.InsertOneAsync(value);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = _mapper.Map<List<ResultProductDto>>(await _productCollection.Find(x => true).ToListAsync());
            foreach (var item in values)
            {
                var category =await _categoryCollection.Find(x =>x.CategoryId==item.CategoryId).FirstOrDefaultAsync();
                item.CategoryName = category.CategoryName;
            }
            return values;
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            return _mapper.Map<GetByIdProductDto>(await _productCollection.Find(x =>x.ProductId==id).FirstOrDefaultAsync());
        }

        public async Task RemoveProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            var storageName = Guid.NewGuid().ToString() + "-" + updateProductDto.ImageFile.FileName;
            value.ImageUrl = await _gcsservice.UploadFileAsync(updateProductDto.ImageFile, storageName);
            value.StorageName = storageName;
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, value);
        }
    }
}
