using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DynamoDb.Interfaces
{
    public interface IBlogPostService
    {
        abstract BlogPostModel GetBlogPostById(string id);
        abstract IEnumerable<BlogPostModel> GetBlogPostsList();
    }
}
