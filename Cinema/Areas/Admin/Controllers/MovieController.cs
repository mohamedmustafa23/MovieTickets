using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTickets.Data;
using MovieTickets.Models;
using MovieTickets.ViewModels;

namespace MovieTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context = new();


        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var movieVM = new MovieVM
            {
                Categories = _context.Categories,
                CinemaHalls = _context.CinemaHalls,
                Actors = _context.Actors
            };

            return View(movieVM);
        }

        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile img, int[] ActorIds)
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

            movie.MovieActors = new List<MovieActor>();
            foreach (var actorId in ActorIds)
            {
                movie.MovieActors.Add(new MovieActor
                {
                    ActorId = actorId,
                    Movie = movie
                });
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
            var movie = _context.Movies
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefault(a => a.Id == id);

            if (movie == null) return NotFound();

            var movieVM = new MovieVM
            {
                Categories = _context.Categories,
                CinemaHalls = _context.CinemaHalls,
                Actors = _context.Actors,
                Movie = movie
            };

            return View(movieVM);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile? img, int[] ActorIds)
        {
            var existingMovie = _context.Movies
                .Include(m => m.MovieActors)
                .FirstOrDefault(a => a.Id == movie.Id);

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

                existingMovie.MovieActors.Clear();
                foreach (var actorId in ActorIds)
                {
                    existingMovie.MovieActors.Add(new MovieActor
                    {
                        ActorId = actorId,
                        MovieId = existingMovie.Id
                    });
                }

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
