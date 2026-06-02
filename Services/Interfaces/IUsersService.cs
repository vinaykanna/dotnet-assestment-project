using NewsApi.DTOs;

namespace NewsApi.Services.Interfaces;

public interface IUsersService
{
    Task<UserResponseDto?> CreateUser(UserDto userDto);
    Task<string?> LoginUser(LoginDto loginDto);
}