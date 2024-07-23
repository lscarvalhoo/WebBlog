using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebBlog.Authentication.Models;
using WebBlog.Authentication.Services;

namespace WebBlog.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel request)
        {
            if (await _userService.UserExists(request.Username))
                return BadRequest("Username already exists");

            await _userService.RegisterUser(request.Username, request.Password);
            return Ok("User registered successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            var user = await _userService.AuthenticateUser(request.Username, request.Password);
            if (user == null)
                return Unauthorized();

            var claims = new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) };

            var tokenString = _userService.GenerateToken(claims);

            return Ok(new { Token = tokenString });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateModel request)
        {
            var user = await _userService.UpdateUser(id, request.Username, request.Password);
            if (user == null)
                return NotFound();

            return Ok("User updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (!result)
                return NotFound();

            return Ok("User deleted successfully");
        }
    }
}
