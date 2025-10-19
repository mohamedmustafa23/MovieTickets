using Microsoft.AspNetCore.Mvc;
using MovieTickets.Models;
using MovieTickets.Data;

namespace MovieTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile img)
        {
            if (img != null && img.Length > 0)
            {
                var fileName = Path.GetFileName(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Movies", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                movie.MainImg = "/Images/Movies/" + fileName;
            }
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(a => a.Id == id);
            if (movie != null)
            {
                if (!string.IsNullOrEmpty(movie.MainImg))
                {
                    var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", movie.MainImg.TrimStart('/'));
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(a => a.Id == id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile? img)
        {
            var existingMovie = _context.Movies.FirstOrDefault(a => a.Id == movie.Id);
            if (existingMovie is not null)
            {
                if (img is not null && img.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingMovie.MainImg))
                    {
                        var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingMovie.MainImg.TrimStart('/'));
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }
                    var fileName = Path.GetFileName(img.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Movies", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    existingMovie.MainImg = "/Images/Movies/" + fileName;
                }

                existingMovie.Name = movie.Name;
                existingMovie.Price = movie.Price;
                existingMovie.DateTime = movie.DateTime;
                existingMovie.Status = movie.Status;
                existingMovie.CategoryId = movie.CategoryId;
                existingMovie.CinemaHallId = movie.CinemaHallId;
                existingMovie.Description = movie.Description;

                _context.Movies.Update(existingMovie);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
