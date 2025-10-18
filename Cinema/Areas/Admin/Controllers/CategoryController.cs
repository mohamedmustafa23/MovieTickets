using Microsoft.AspNetCore.Mvc;
using MovieTickets.Models;
using MovieTickets.Data;

namespace MovieTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            _context.Categories.Add(Category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Category = _context.Categories.FirstOrDefault(a => a.Id == id);
            if (Category != null)
            {
                _context.Categories.Remove(Category);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Category = _context.Categories.FirstOrDefault(a => a.Id == id);
            return View(Category);
        }
        [HttpPost]
        public IActionResult Edit(Category Category)
        {
            var existingCategory = _context.Categories.FirstOrDefault(a => a.Id == Category.Id);
            if (existingCategory is not null)
            {
                existingCategory.Name = Category.Name;
                existingCategory.Description = Category.Description;

                _context.Categories.Update(existingCategory);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
