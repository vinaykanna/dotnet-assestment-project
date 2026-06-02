
using NewsApi.Models;
using NewsApi.Repositories;
using NewsApi.DTOs;
using Microsoft.AspNetCore.Identity;
using NewsApi.Enums;
using NewsApi.Authentication;
using NewsApi.Exceptions;

namespace NewsApi.Services;

public interface IUsersService
{
    Task<UserResponseDto?> CreateUser(UserDto userDto);

    Task<string?> LoginUser(LoginDto loginDto);
}

public class UsersService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtTokenGenerator jwtTokenGenerator) : IUsersService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserResponseDto?> CreateUser(UserDto userDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException(userDto.Email);
        }

        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
        };

        user.Password = passwordHasher.HashPassword(user, userDto.Password);
        user.Role = Role.User;

        if (userDto.IsAuthor)
        {
            user.Role = Role.Author;
        }

        var newUser = await _userRepository.CreateAsync(user);

        return new UserResponseDto
        {
            Id = newUser!.Id,
            Name = newUser.Name,
            Email = newUser.Email
        };
    }

    public async Task<string?> LoginUser(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return null;
        }

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.Password,
            loginDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return jwtTokenGenerator.GenerateToken(user);
    }
}