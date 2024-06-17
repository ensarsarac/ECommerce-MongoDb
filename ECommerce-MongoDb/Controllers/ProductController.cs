using ECommerce_MongoDb.Dtos.CategoryDtos;
using ECommerce_MongoDb.Dtos.ProductDtos;
using ECommerce_MongoDb.Services.CategoryService;
using ECommerce_MongoDb.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce_MongoDb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult> ProductList()
        {
            return View(await _productService.GetAllProductAsync());
        }
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
            return View();
        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.RemoveProductAsync(id);
            return RedirectToAction("ProductList");
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
            var value = await _productService.GetByIdProductAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList");
        }
    }
}
