using WebBlog.Authentication.Repositories;
using WebBlog.Utils;

namespace WebBlog.Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userRepository.UserExists(username);
        }

        public async Task<User> RegisterUser(string username, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Username = username,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt)
            };

            await _userRepository.AddUser(user);
            return user;
        }

        public async Task<User> UpdateUser(int id, string username, string password)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return null;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                PasswordHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = Convert.ToBase64String(passwordHash);
                user.PasswordSalt = Convert.ToBase64String(passwordSalt);
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                user.Username = username;
            }

            await _userRepository.UpdateUser(user);
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return false;

            await _userRepository.DeleteUser(user);
            return true;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
                return null;

            if (!PasswordHelper.VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                return null;

            return user;
        }
    }
}
