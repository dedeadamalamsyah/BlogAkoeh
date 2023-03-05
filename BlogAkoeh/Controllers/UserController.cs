using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAkoeh.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var claim = HttpContext.User.Claims;
            var userFullname = claim.Where(x => x.Type == "name").FirstOrDefault()?.Value;

            ViewData["Fullname"] = userFullname;

            return View();
        }
    }
}
