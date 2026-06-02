
using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Repositories;

public interface IUserRepository
{
    Task<User?> CreateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}
public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<User?> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return user;
    }


    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}