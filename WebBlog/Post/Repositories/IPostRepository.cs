using WebBlog.Post.DTO;
using WebBlog.Post.Models;

namespace WebBlog.Post.Repositories
{
    public interface IPostRepository
    {
        Task Add(UserPost post);
        Task<IEnumerable<UserPost>> GetAll();
        Task<UserPost> GetById(Guid id);
        Task Update(UserPost post);
        Task Delete(UserPost post);
    }
}
