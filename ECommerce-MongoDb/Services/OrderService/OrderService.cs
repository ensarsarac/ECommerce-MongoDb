using AutoMapper;
using ECommerce_MongoDb.Dtos.CategoryDtos;
using ECommerce_MongoDb.Dtos.OrderDtos;
using ECommerce_MongoDb.Entities;
using ECommerce_MongoDb.Settings;
using MongoDB.Driver;

namespace ECommerce_MongoDb.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, IDatabaseSetting _databaseSetting)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSetting.ConnectionString);
            var database = client.GetDatabase(_databaseSetting.DatabaseName);
            _orderCollection = database.GetCollection<Order>(_databaseSetting.OrderCollectionName);
            _customerCollection = database.GetCollection<Customer>(_databaseSetting.CustomerCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSetting.ProductCollectionName);
        }
        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var product = await _productCollection.Find(x => x.ProductId == createOrderDto.ProductId).FirstOrDefaultAsync();
            if(createOrderDto.Amount <= product.Stock)
            {
                createOrderDto.TotalPrice = product.Price * createOrderDto.Amount;
                await _orderCollection.InsertOneAsync(_mapper.Map<Order>(createOrderDto));
            }
            else
            {
                createOrderDto.Amount = product.Stock;
                createOrderDto.TotalPrice = product.Price * createOrderDto.Amount;
                await _orderCollection.InsertOneAsync(_mapper.Map<Order>(createOrderDto));
            }
            
        }

        public async Task DescreateProductAmount(string id)
        {
            var order = await _orderCollection.Find(x => x.OrderId == id).FirstOrDefaultAsync();
            var product = await _productCollection.Find(x => x.ProductId == order.ProductId).FirstOrDefaultAsync();
            if(order.Amount < 2)
            {
                await _orderCollection.DeleteOneAsync(x => x.OrderId == id);
            }
            else
            {
                order.Amount -= 1;
                order.TotalPrice = product.Price * order.Amount;
                await _orderCollection.FindOneAndReplaceAsync(x => x.OrderId == id, order);
            }
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            var values = await _orderCollection.Find(x => true).ToListAsync();
            var result = _mapper.Map<List<ResultOrderDto>>(values);
            foreach (var item in result)
            {
                var customer = await _customerCollection.Find(x => x.CustomerId == item.CustomerId).FirstOrDefaultAsync();
                var product = await _productCollection.Find(x => x.ProductId == item.ProductId).FirstOrDefaultAsync();
                item.CustomerPhone = customer.Phone;
                item.CustomerNameSurname = customer.CustomerNameSurname;
                item.CustomerAddress = customer.Address;
                item.ProductImage = product.ImageUrl;
                item.ProductPrice = product.Price;
                item.ProductName = product.Name;
                item.ProductStock = product.Stock;
            }
            return result;
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(string id)
        {
            var order = await _orderCollection.Find(x=>x.OrderId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOrderDto>(order);
        }

        public async Task IncreaseProductAmount(string id)
        {
            var order = await _orderCollection.Find(x => x.OrderId == id).FirstOrDefaultAsync();
            var product = await _productCollection.Find(x=>x.ProductId==order.ProductId).FirstOrDefaultAsync();
            if(product.Stock > order.Amount)
            {
                order.Amount += 1;
                order.TotalPrice = product.Price * order.Amount;
                await _orderCollection.FindOneAndReplaceAsync(x => x.OrderId == id, order);
            }
            else
            {
                order.Amount = product.Stock;
            }
        }

        public async Task RemoveOrderAsync(string id)
        {
            await _orderCollection.DeleteOneAsync(x=> x.OrderId == id); 
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            await _orderCollection.FindOneAndReplaceAsync(x => x.OrderId == updateOrderDto.OrderId, _mapper.Map<Order>(updateOrderDto));
        }
    }
}
