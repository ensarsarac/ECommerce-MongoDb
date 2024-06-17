using AutoMapper;
using ECommerce_MongoDb.Dtos.CategoryDtos;
using ECommerce_MongoDb.Entities;
using ECommerce_MongoDb.Settings;
using MongoDB.Driver;

namespace ECommerce_MongoDb.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSetting _databaseSetting)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSetting.ConnectionString);
            var database = client.GetDatabase(_databaseSetting.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSetting.CategoryCollectionName);
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(createCategoryDto));
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            return _mapper.Map<List<ResultCategoryDto>>(await _categoryCollection.Find(x => true).ToListAsync());
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            return _mapper.Map<GetByIdCategoryDto>(await _categoryCollection.Find(x =>x.CategoryId==id).FirstOrDefaultAsync());
        }

        public async Task RemoveCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x=>x.CategoryId==id);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryId==updateCategoryDto.CategoryId,_mapper.Map<Category>(updateCategoryDto));
        }
    }
}
