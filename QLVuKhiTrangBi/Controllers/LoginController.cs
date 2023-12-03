using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QLVuKhiTrangBi.Data;
using System.Security.Claims;
using QLVuKhiTrangBi.Models;
using QLVuKhiTrangBi.Services;

namespace QLVuKhiTrangBi.Controllers
{
    public class LoginController : Controller
    {
        QlvuKhiTrangBiContext db = new QlvuKhiTrangBiContext();
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var tk = db.TaiKhoans.FirstOrDefault(t => t.TenDn == userName && t.MatKhau == password);

            if (tk != null)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("UserName", userName);

                var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, tk.HoTen),
                   new Claim(ClaimTypes.Sid, tk.MaQn),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(5)
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("IsLoggedIn");
            HttpContext.Session.Remove("UserName");

            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
            
            HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            HttpContext.Response.Headers["Pragma"] = "no-cache";
            HttpContext.Response.Headers["Expires"] = "0";

            return RedirectToAction("Index", "Login"); 
        }
    }
}
