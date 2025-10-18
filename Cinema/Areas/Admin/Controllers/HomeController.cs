using Microsoft.AspNetCore.Mvc;
using MovieTickets.Data;
using MovieTickets.ViewModels;
namespace MovieTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var viewModel = new AdminHomeVM
            {
                Movies = _context.Movies.ToList(),
                Actors = _context.Actors.ToList(),
                Categories = _context.Categories.ToList(),
                CinemaHalls = _context.CinemaHalls.ToList()
            };

            return View(viewModel);
        }
        public IActionResult ShowActors()
        {
            var Actors = _context.Actors.ToList();
            return View(Actors);
        }
        public IActionResult ShowMovies()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }
        public IActionResult ShowCinemaHalls()
        {
            var cinemaHalls = _context.CinemaHalls.ToList();
            return View(cinemaHalls);
        }
        public IActionResult ShowCategories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
