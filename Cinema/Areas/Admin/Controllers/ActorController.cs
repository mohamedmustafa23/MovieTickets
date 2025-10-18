using Microsoft.AspNetCore.Mvc;
using MovieTickets.Models;
using MovieTickets.Data;

namespace MovieTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var actors = _context.Actors.ToList();
            return View(actors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Actor actor, IFormFile img)
        {
            if (img != null && img.Length > 0)
            {
                var fileName = Path.GetFileName(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Actors", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                actor.Img = "/Images/Actors/" + fileName;
            }
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Id == id);
            if (actor != null)
            {
                if (!string.IsNullOrEmpty(actor.Img))
                {
                    var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", actor.Img.TrimStart('/'));
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }
                _context.Actors.Remove(actor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Id == id);
            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile? img)
        {
            var existingActor = _context.Actors.FirstOrDefault(a => a.Id == actor.Id);
            if (existingActor is not null)
            {
                if (img is not null && img.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingActor.Img))
                    {
                        var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingActor.Img.TrimStart('/'));
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }
                    var fileName = Path.GetFileName(img.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Actors", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    existingActor.Img = "/Images/Actors/" + fileName;
                }

                existingActor.Name = actor.Name;
                existingActor.Email = actor.Email;
                existingActor.DateOfBirth = actor.DateOfBirth;
                existingActor.Gender = actor.Gender;
                existingActor.Description = actor.Description;

                _context.Actors.Update(existingActor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
