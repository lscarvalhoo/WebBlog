using WebBlog.Post.DTO;
using WebBlog.Post.Models;
using WebBlog.Post.Repositories;

namespace WebBlog.Post.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository; 

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository; 
        }

        public async Task<PostDTO> GetById(Guid id)
        {
            var post = await _postRepository.GetById(id);
            if (post == null) return null;

            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Username = post.User.Username
            };
        }

        public async Task Add(UserPost post)
        {
            await _postRepository.Add(post);
        }

        public async Task<IEnumerable<PostDTO>> GetAll()
        {
            var posts = await _postRepository.GetAll();
            return posts.Select(post => new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Username = post.User.Username
            });
        }  

        public async Task Update(UserPost post)
        {
            await _postRepository.Update(post);
        }

        public async Task Delete(Guid id)
        {
            var post = await _postRepository.GetById(id);
            if (post != null)
            {
                await _postRepository.Delete(post);
            }
        }

        public async Task<UserPost> Get(Guid id)
        {
            return await _postRepository.GetById(id);
        }
    }
}
