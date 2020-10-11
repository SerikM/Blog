using System.Collections.Generic;
using BlogReact.Services.Interfaces;
using Data.Aurora.Entities;
using Data.Aurora.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogReact.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(ILogger<BlogPostController> logger, IBlogPostService blogPostService)
        {
            _logger = logger;
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            return _blogPostService.GetBlogPostsList(10, 0);
        }
    }
}
