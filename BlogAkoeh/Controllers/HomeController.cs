using BlogAkoeh.Data;
using BlogAkoeh.Models;
using BlogAkoeh.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogAkoeh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MysqlContext _context;
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _env;

        public HomeController(
            ILogger<HomeController> logger,
            MysqlContext c, EmailService e, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = c;
            _emailService = e;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var EmailData = new MailData()
            {
                To = "dedeadamalamsyah9@gmail.com",
                Subject = "Test Email #1",
                //Message = "Demo Email"
                Message = System.IO.File.ReadAllText(
                    _env.WebRootPath + "\\template\\email.html")
            };

            await _emailService.SendAsync(EmailData);

            List<Post> posts = _context.Posts.ToList();
            return View(posts);
        }

        public IActionResult Detail(int id)
        {
            Post post = _context.Posts.Where(x => x.Id == id).FirstOrDefault();
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