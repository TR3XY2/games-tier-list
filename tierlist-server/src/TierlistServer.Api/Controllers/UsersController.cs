using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TierlistServer.Application.DTOs.Users;
using TierlistServer.Application.Interfaces;
using TierlistServer.Domain.Entities;

namespace TierlistServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var user = await _userService.RegisterAsync(dto.Email, dto.Username, dto.Password);

            var userDto = this.mapper.Map<UserDto>(user);

            return this.Ok(userDto);
        }
        catch (InvalidOperationException ex)
        {
            return this.BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userService.LoginAsync(dto.Email, dto.Password);
        if (user == null)
        {
            return this.Unauthorized(new { message = "Invalid credentials" });
        }

        var userDto = this.mapper.Map<UserDto>(user);

        return this.Ok(userDto);
    }
}
