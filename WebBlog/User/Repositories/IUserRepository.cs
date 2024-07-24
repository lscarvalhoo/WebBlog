namespace WebBlog.Authentication.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Exists(string username);
        Task<User> GetByUsername(string username); 
        Task AddUser(User user); 
    }
}
