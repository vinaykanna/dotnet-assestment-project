
using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> CreateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}