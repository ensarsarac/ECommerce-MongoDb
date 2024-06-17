using ECommerce_MongoDb.Dtos.CategoryDtos;
using ECommerce_MongoDb.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_MongoDb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList()
        {
            return View(await _categoryService.GetAllCategoryAsync());
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("CategoryList");
        }
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.RemoveCategoryAsync(id);
            return RedirectToAction("CategoryList");
        }
        public async Task<IActionResult> UpdateCategory(string id)
        {
            return View(await _categoryService.GetByIdCategoryAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return RedirectToAction("CategoryList");
        }
    }
}
