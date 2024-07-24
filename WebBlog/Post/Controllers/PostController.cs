using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebBlog.Post.DTO;
using WebBlog.Post.Models;
using WebBlog.Post.Services;

namespace WebBlog.Post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetAllPosts")] 
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAll();
            if (posts.IsNullOrEmpty()) 
                return NotFound();

            return Ok(posts);
        }

        [HttpGet("GetPostById/{id}")] 
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] PostModel request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (userId == null)
                return Unauthorized();

            var post = new UserPost
            {
                Title = request.Title,
                Content = request.Content,
                UserId = Guid.Parse(userId)
            };

            await _postService.Add(post);
            return Ok("Post created");
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] PostModel request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (userId == null)
                return Unauthorized();

            var post = await _postService.Get(id);
            if (post == null)
                return NotFound("Post not found");

            post.Title = request.Title;
            post.Content = request.Content;

            await _postService.Update(post);
            return Ok("Post updated");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (userId == null)
                return Unauthorized();

            var post = await _postService.GetById(id);
            if (post == null)
                return NotFound("Post not found");
            
            await _postService.Delete(id);
            return Ok("Post deleted");
        }
    }
}
