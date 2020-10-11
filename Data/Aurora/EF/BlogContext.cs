using Data.Aurora.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Aurora.EF
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
          : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }



        DbSet<BlogPost> BLOG_POSTS { get; set; }
    }
}
