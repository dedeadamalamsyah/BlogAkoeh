using BlogAkoeh.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAkoeh.Data
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
