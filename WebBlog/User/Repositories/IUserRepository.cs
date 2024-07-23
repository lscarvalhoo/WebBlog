namespace WebBlog.Authentication.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
