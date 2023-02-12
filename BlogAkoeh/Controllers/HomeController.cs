using BlogAkoeh.Data;
using BlogAkoeh.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogAkoeh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MysqlContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            MysqlContext c)
        {
            _logger = logger;
            _context = c;
        }

        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.ToList();
            return View(posts);
        }

        public IActionResult Detail(int id)
        {
            Post post = _context.Posts.Where(x => x.Id == id)
                .FirstOrDefault();
            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}