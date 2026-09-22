using BulkyBook.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.Business.Services.IServices;


namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(Category category)
        {
        
            if(!String.IsNullOrEmpty(category.Name) &&
                await _categoryService.CategoryExistsAsync(category.Name, category.Id))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
            }
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(category);
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            if (!String.IsNullOrEmpty(category.Name) && await _categoryService.CategoryExistsAsync(category.Name, category.Id))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
            }
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);   
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategoryAsync(id.Value);
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
