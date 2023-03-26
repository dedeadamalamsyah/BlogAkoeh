using BlogAkoeh.Data;
using BlogAkoeh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly MysqlContext _context;

        public PostController(MysqlContext c)
        {
            _context = c;
        }

        [HttpGet("")]
        public IActionResult GetAllPost()
        {
            var posts = _context.Posts.ToList();

            return Ok(posts);
        }

        [HttpGet("search")]
        public IActionResult SearchPost([FromQuery] string keyword)
        {
            var post = _context.Posts.Where(x => x.Title.ToLower().Contains(keyword) || x.Content.ToLower().Contains(keyword)).ToList();
            //startswith = mencari keyword di depan, endswith kebalikannya

            return Ok(post);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostByID(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);

            if (post == null)
            {
                return BadRequest("Post not found");
            }

            return Ok(post);
        }

        [HttpPost("create")]
        public IActionResult AddPost([FromBody] Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();

            return Ok(post);
            //return NoContent();
        }

        [HttpPut("update")]
        //[HttpPatch("update")]
        public IActionResult UpdatePost([FromBody] Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();

            return Ok(post);
            //return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            _context.Posts.Remove(post);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
