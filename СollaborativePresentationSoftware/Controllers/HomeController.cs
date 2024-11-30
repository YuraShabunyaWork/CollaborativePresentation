using Microsoft.AspNetCore.Mvc;
using СollaborativePresentationSoftware.Data;
using СollaborativePresentationSoftware.Models;

namespace СollaborativePresentationSoftware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<User> users = _context.Users.ToList();
            return View(users);
        }

    }
}
