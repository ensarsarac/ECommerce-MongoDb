using ECommerce_MongoDb.Dtos.OrderDtos;
using ECommerce_MongoDb.Services.CustomerService;
using ECommerce_MongoDb.Services.OrderService;
using ECommerce_MongoDb.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce_MongoDb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, ICustomerService customerService, IProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
        }

        public async Task<IActionResult> OrderList()
        {
            var values = await _orderService.GetAllOrderAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateOrder()
        {
            ViewBag.products = new SelectList(await _productService.GetAllProductAsync(),"ProductId","Name");  
            ViewBag.customers = new SelectList(await _customerService.GetAllCustomerAsync(),"CustomerId","CustomerNameSurname");  
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            await _orderService.CreateOrderAsync(createOrderDto);
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> DeacreaseAmount(string id)
        {
            await _orderService.DescreateProductAmount(id);
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> IncreaseAmount(string id)
        {
            await _orderService.IncreaseProductAmount(id);
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> DeleteOrder(string id)
        {
            await _orderService.RemoveOrderAsync(id);
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> UpdateOrder(string id)
        {
            ViewBag.products = new SelectList(await _productService.GetAllProductAsync(), "ProductId", "Name");
            ViewBag.customers = new SelectList(await _customerService.GetAllCustomerAsync(), "CustomerId", "CustomerNameSurname");
            return View(await _orderService.GetByIdOrderAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            await _orderService.UpdateOrderAsync(updateOrderDto);
            return RedirectToAction("OrderList");
        }
    }
}
