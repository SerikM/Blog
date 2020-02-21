using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController : ControllerBase
    {
        private static readonly string[] BlogTopics= new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(ILogger<BlogPostController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BlogPostModel> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new BlogPostModel
            {
                Date = DateTime.Now.AddDays(index),
                Topic = BlogTopics[rng.Next(BlogTopics.Length)], 
                Contnet = "some dummy content"
            })
            .ToArray();
        }
    }
}
