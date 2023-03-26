using BlogAkoeh.Data;
using BlogAkoeh.Models;
using BlogAkoeh.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace BlogAkoeh.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IWebHostEnvironment _env;

        public UserController(MysqlContext c, IWebHostEnvironment env)
        {
            _context = c;
            _env = env;
        }

        public IActionResult Index()
        {
            var users = _context.Users.Include(x => x.Role).ToList();

            return View(users);
        }

        public IActionResult Detail(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            
            return View(user);
        }

        public IActionResult Download(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            var filepath = Path.Combine(_env.WebRootPath, "upload", user.Photo);

            return File(System.IO.File.ReadAllBytes(filepath), "image/png", Path.GetFileName(filepath));
        }

        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] UserForm UserForm, IFormFile Photo)
        {
            if(Photo.Length > 100000)
            {
                ModelState.AddModelError(nameof(UserForm.Photo), "Photo size is too large");
            }

            if(!ModelState.IsValid)
            {
                return View();
            }

            var filename = "photo_" + UserForm.Username + Path.GetExtension(Photo.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "upload", filename);

            using (var stream = System.IO.File.Create(filepath))
            {
                Photo.CopyTo(stream);
            }

            var role = _context.Roles.FirstOrDefault(x => x.Id == UserForm.Role);

            var User = new User()
            {
                Username = UserForm .Username,
                Password = UserForm.Password,
                Fullname = UserForm.Fullname,
                Photo = filename,
                Role = role
            };

            UserForm.Photo = filename;

            _context.Users.Add(User);
            _context.SaveChanges();

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}
