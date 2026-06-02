
using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;

namespace NewsApi.Repositories;

public class FavouriteArticleRepository(AppDbContext context) : IFavouriteArticleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<FavouriteArticle?> CreateAsync(FavouriteArticle favouriteArticle)
    {
        await _context.FavouriteArticles.AddAsync(favouriteArticle);
        await _context.SaveChangesAsync();
        return favouriteArticle;
    }

    public async Task<List<FavouriteArticle>> GetByUserIdAsync(Guid userId)
    {
        return await _context.FavouriteArticles
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Include(x => x.Article)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
    }

    public async Task<FavouriteArticle?> GetAsync(Guid id)
    {
        var favouriteArticle = await _context.FavouriteArticles
                .AsNoTracking()
                .Include(x => x.Article)
                .FirstOrDefaultAsync(x => x.Id == id);

        return favouriteArticle;
    }

    public async Task<FavouriteArticle?> DeleteAsync(FavouriteArticle favouriteArticle)
    {
        _context.FavouriteArticles.Remove(favouriteArticle);
        await _context.SaveChangesAsync();
        return favouriteArticle;
    }
}