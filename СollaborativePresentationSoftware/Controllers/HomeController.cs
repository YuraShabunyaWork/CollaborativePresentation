using Microsoft.AspNetCore.Mvc;
using PresentationApp.Data;
using PresentationApp.Models;
using PresentationApp.Services.Interfases;

namespace PresentationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _userService.GetUsersAsync();
            return View(users);
        }

    }
}
