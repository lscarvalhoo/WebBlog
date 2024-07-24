using Microsoft.EntityFrameworkCore;
using WebBlog.Post.DTO;
using WebBlog.Post.Models;

namespace WebBlog.Post.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseConfig _context;

        public PostRepository(DatabaseConfig context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserPost>> GetAll()
        {
            return await _context.UserPost.Include(p => p.User).ToListAsync();
        }

        public async Task<UserPost> GetById(Guid id)
        {
            return await _context.UserPost.Include(p => p.User).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(UserPost post)
        {
            _context.UserPost.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserPost post)
        {
            _context.UserPost.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(UserPost post)
        {
            _context.UserPost.Remove(post);
            await _context.SaveChangesAsync();
        } 
    }
}
