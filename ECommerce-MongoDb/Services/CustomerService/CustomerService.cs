using AutoMapper;
using ECommerce_MongoDb.Dtos.CategoryDtos;
using ECommerce_MongoDb.Dtos.CustomerDtos;
using ECommerce_MongoDb.Entities;
using ECommerce_MongoDb.Settings;
using MongoDB.Driver;

namespace ECommerce_MongoDb.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper, IDatabaseSetting _databaseSetting)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSetting.ConnectionString);
            var database = client.GetDatabase(_databaseSetting.DatabaseName);
            _customerCollection = database.GetCollection<Customer>(_databaseSetting.CustomerCollectionName);
        }
        public async Task CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            await _customerCollection.InsertOneAsync(_mapper.Map<Customer>(createCustomerDto));
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            return _mapper.Map<List<ResultCustomerDto>>(await _customerCollection.Find(x => true).ToListAsync());
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id)
        {
            return _mapper.Map<GetByIdCustomerDto>(await _customerCollection.Find(x => x.CustomerId == id).FirstOrDefaultAsync());
        }

        public async Task RemoveCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(x=>x.CustomerId == id);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            await _customerCollection.FindOneAndReplaceAsync(x => x.CustomerId == updateCustomerDto.CustomerId, _mapper.Map<Customer>(updateCustomerDto));
        }
    }
}
