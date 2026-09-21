using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name.ToLower().Trim() == category.DisplayOrder.ToString().ToLower().Trim())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if(!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "A category with this name already exists.");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
