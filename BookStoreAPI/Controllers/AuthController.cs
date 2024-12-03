using BookStoreAPI.Models;
//using JwtAuthDemo.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        var fakeUsers = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "user", Password = "user123", Role = "User" }
        };

        var authenticatedUser = fakeUsers.FirstOrDefault(u =>
            u.Username == user.Username && u.Password == user.Password);

        if (authenticatedUser == null) return Unauthorized();

        var token = _tokenService.GenerateToken(authenticatedUser.Username, authenticatedUser.Role);
        return Ok(new { Token = token });
    }
}