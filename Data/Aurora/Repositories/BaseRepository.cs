using Data.Aurora.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Aurora.Interfaces
{
    public interface BaseRepository
    {
        abstract BlogPost GetBlogPostById(string id);
        abstract IEnumerable<BlogPost> GetBlogPostsList();
    }
}
