using BlogAkoeh.Data;
using BlogAkoeh.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAkoeh.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly MysqlContext _context;

        public UserController(MysqlContext c)
        {
            _context = c;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            _context.Users.Remove(user);
            _context.SaveChanges();

            return View("Index");
        }
    }
}
