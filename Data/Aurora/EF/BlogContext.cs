using Microsoft.EntityFrameworkCore;

namespace Data.Aurora.EF
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
          : base(options)
        { }
    }
}
