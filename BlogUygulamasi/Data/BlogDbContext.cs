using BlogUygulamasi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogUygulamasi.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)        
        {
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // SQLite ve MsSql için Connection String
        //    optionsBuilder.UseSqlite("Data Source=blog.db");// optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=blog;Trusted_Connection=True;");
        //}
    }
}
