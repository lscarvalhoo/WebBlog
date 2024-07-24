using WebBlog.Post.DTO;
using WebBlog.Post.Models;

namespace WebBlog.Post.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAll();
        Task<PostDTO> GetById(Guid id);
        Task<UserPost> Get(Guid id);
        Task Add(UserPost post);
        Task Update(UserPost post);
        Task Delete(Guid id);
    }
}
