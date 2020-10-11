using Data.Aurora.Entities;
using System;
using System.Collections.Generic;

namespace BlogReact.Services.Interfaces
{
    public interface IBlogPostService
    {
        public IEnumerable<BlogPost> GetBlogPostsList(int numToTake, int start);
    }
}
