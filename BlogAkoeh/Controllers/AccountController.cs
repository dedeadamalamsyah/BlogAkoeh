using BlogAkoeh.Data;
using BlogAkoeh.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAkoeh.Controllers
{
    public class AccountController : Controller
    {
        private readonly MysqlContext _context;
        public AccountController(MysqlContext c)
        {
            _context = c;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login login)
        {
            var user = _context.Users
                .Where(x => x.Username == login.Username && x.Password == login.Password)
                .FirstOrDefault();

            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim("username", user.Username),
                    new Claim("name", user.Fullname),
                    new Claim("role", "Admin")
                };

                var identity = new ClaimsIdentity(claims, "Cookie", "name", "role");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return Redirect("/User/Index");
            }

            return View();
        }
    }
}
