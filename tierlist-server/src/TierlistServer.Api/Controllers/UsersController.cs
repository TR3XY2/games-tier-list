using Microsoft.AspNetCore.Mvc;
using TierlistServer.Application.Services;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto.Email, dto.Username, dto.Password);
                return Ok(new { user.Id, user.Email, user.Username });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto.Email, dto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new { user.Id, user.Email, user.Username });
        }
    }

    public record RegisterDto(string Email, string Username, string Password);
    public record LoginDto(string Email, string Password);
}
