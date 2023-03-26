using BlogAkoeh.Data;
using BlogAkoeh.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAkoeh.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly MysqlContext _context;

        public PostController(MysqlContext c)
        {
            _context = c;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();

            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);

            return View(post);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
