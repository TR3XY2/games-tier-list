using Microsoft.AspNetCore.Mvc;
using TierlistServer.Application.DTOs.Users;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
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
}
