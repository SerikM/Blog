using BlogReact.Services.Interfaces;
using Data.Aurora.Entities;
using Data.Aurora.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogReact.Services.Implementations
{
    
    public class BlogPostService : IBlogPostService
    {
        private readonly IBaseRepository<BlogPost> _blogPostRepo;
        public BlogPostService(IBaseRepository<BlogPost> blogPostRepo) 
        {
            _blogPostRepo = blogPostRepo;
        }

        public IEnumerable<BlogPost> GetBlogPostsList(int numToTake, int start)
        {
            return _blogPostRepo.GetBlogPostsList(numToTake, start);
        }
    }
}
