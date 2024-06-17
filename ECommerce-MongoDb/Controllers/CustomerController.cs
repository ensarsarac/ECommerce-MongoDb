using ECommerce_MongoDb.Dtos.CustomerDtos;
using ECommerce_MongoDb.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_MongoDb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.GetAllCustomerAsync();
            return View(values);
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            await _customerService.CreateCustomerAsync(createCustomerDto);
            return RedirectToAction("CustomerList");
        }
        public async Task<IActionResult> UpdateCustomer(string id)
        {
            var customer = await _customerService.GetByIdCustomerAsync(id);
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            await _customerService.UpdateCustomerAsync(updateCustomerDto);
            return RedirectToAction("CustomerList");
        }
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerService.RemoveCustomerAsync(id);
            return RedirectToAction("CustomerList");
        }
    }
}
