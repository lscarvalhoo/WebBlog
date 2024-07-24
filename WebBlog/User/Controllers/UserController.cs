using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebBlog.Authentication.DTO;
using WebBlog.Authentication.Services;

namespace WebBlog.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel request)
        {
            if (await _userService.Exists(request.Username))
                return BadRequest("Username already exists");

            await _userService.Register(request.Username, request.Password);
            return Ok("User registered");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);
            if (user == null)
                return Unauthorized();

            var claims = new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) };

            var tokenString = _userService.GenerateToken(claims);

            return Ok(new { Token = tokenString });
        } 
    }
}
