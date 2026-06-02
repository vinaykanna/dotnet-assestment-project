using Microsoft.AspNetCore.Mvc;
using NewsApi.DTOs;
using NewsApi.Services.Interfaces;

namespace NewsApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserDto userDto)
    {
        var user = await _usersService.CreateUser(userDto);

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var token = await _usersService.LoginUser(loginDto);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new
        {
            AccessToken = token
        });
    }
}