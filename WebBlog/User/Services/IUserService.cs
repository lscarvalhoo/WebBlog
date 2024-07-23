namespace WebBlog.Authentication.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<User> RegisterUser(string username, string password);
        Task<User> UpdateUser(int id, string username, string password);
        Task<bool> DeleteUser(int id);
        Task<User> AuthenticateUser(string username, string password);
    }
}
