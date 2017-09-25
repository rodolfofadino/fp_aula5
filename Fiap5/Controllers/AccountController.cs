using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fiap5.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Fiap5.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            if (user.UserName == "rodolfo" && user.Password == "123Mudar")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                var id = new ClaimsIdentity(claims,"password");
                var principal = new ClaimsPrincipal(id);

                await HttpContext.SignInAsync("app", principal);
            }
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("app");

            return RedirectToAction("Index", "Home");
        }

    }

    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Persistent { get; set; }
    }
}
