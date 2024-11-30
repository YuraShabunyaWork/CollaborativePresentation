using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PresentationApp.Models;
using PresentationApp.ModelsDto;
using PresentationApp.Services.Interfases;
using System.Security.Claims;

namespace PresentationApp.Controllers
{
    public class EnterController : Controller
    {
        private readonly IUserService _userService;

        public EnterController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            if(!await _userService.UserExistsAsync(loginDto.Email))
            {
                return View(loginDto);
            }
            var user = await _userService.GetUserAsync(loginDto.Email);
            if(user.Password != loginDto.Password)
            {
                return View(loginDto);
            }
            user.Status = true;
            user.LastLogin = DateTime.Now;
            if(await _userService.UpdateUserAsync(user))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = loginDto.IsCheck,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }
        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SingIn(SinginDto singinDto)
        {
            if (!ModelState.IsValid)
            {
                return View(singinDto);
            }
            if (await _userService.UserExistsAsync(singinDto.Email))
            {
                return View(singinDto);
            }
            User user = new User()
            {
                Login = singinDto.Login,
                Email = singinDto.Email,
                Password = singinDto.Password,
                Status = true
            };
            
            if (await _userService.CreateUserAsync(user))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        public async Task<IActionResult> LogOut()
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);
            user.Status = false;
            await _userService.UpdateUserAsync(user);
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
