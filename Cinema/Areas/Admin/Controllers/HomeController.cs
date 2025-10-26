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
    }
}
