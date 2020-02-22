using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DynamoDb
{
    public class BlogPostModel
    {
        public string PublishDate { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }
    }
}
