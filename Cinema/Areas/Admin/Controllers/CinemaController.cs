using Microsoft.AspNetCore.Mvc;
using MovieTickets.Models;
using MovieTickets.Data;

namespace MovieTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var CinemaHalls = _context.CinemaHalls.ToList();
            return View(CinemaHalls);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CinemaHall CinemaHall, IFormFile img)
        {
            if (img != null && img.Length > 0)
            {
                var fileName = Path.GetFileName(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/CinemaHalls", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                CinemaHall.Img = "/Images/CinemaHalls/" + fileName;
            }
            _context.CinemaHalls.Add(CinemaHall);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var CinemaHall = _context.CinemaHalls.FirstOrDefault(a => a.Id == id);
            if (CinemaHall != null)
            {
                if (!string.IsNullOrEmpty(CinemaHall.Img))
                {
                    var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", CinemaHall.Img.TrimStart('/'));
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }
                _context.CinemaHalls.Remove(CinemaHall);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var CinemaHall = _context.CinemaHalls.FirstOrDefault(a => a.Id == id);
            return View(CinemaHall);
        }
        [HttpPost]
        public IActionResult Edit(CinemaHall CinemaHall, IFormFile? img)
        {
            var existingCinemaHall = _context.CinemaHalls.FirstOrDefault(a => a.Id == CinemaHall.Id);
            if (existingCinemaHall is not null)
            {
                if (img is not null && img.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingCinemaHall.Img))
                    {
                        var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCinemaHall.Img.TrimStart('/'));
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }
                    var fileName = Path.GetFileName(img.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/CinemaHalls", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    existingCinemaHall.Img = "/Images/CinemaHalls/" + fileName;
                }

                existingCinemaHall.Name = CinemaHall.Name;
                existingCinemaHall.Description = CinemaHall.Description;

                _context.CinemaHalls.Update(existingCinemaHall);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
