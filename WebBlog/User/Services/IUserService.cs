using System.Security.Claims;

namespace WebBlog.Authentication.Services
{
    public interface IUserService
    {
        Task<bool> Exists(string username);
        Task<User> Register(string username, string password); 
        Task<User> Authenticate(string username, string password);
        string GenerateToken(Claim[] claims);
    }
}
