using Microsoft.EntityFrameworkCore;

namespace WebBlog.Authentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConfig _context;

        public UserRepository(DatabaseConfig context)
        {
            _context = context;
        }

        public async Task<bool> Exists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        } 

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
