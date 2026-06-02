
using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;

namespace NewsApi.Repositories;

public interface IFavouriteArticleRepository
{
    Task<List<FavouriteArticle>> GetAllAsync();

    Task<FavouriteArticle?> CreateAsync(FavouriteArticle produdct);

    Task<FavouriteArticle?> DeleteAsync(Guid id);
}
public class FavouriteArticleRepository(AppDbContext context) : IFavouriteArticleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<FavouriteArticle?> CreateAsync(FavouriteArticle favouriteArticle)
    {
        await _context.FavouriteArticles.AddAsync(favouriteArticle);
        await _context.SaveChangesAsync();
        return favouriteArticle;
    }

    public async Task<List<FavouriteArticle>> GetAllAsync()
    {
        return await _context.FavouriteArticles
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
    }

    public async Task<FavouriteArticle?> DeleteAsync(Guid id)
    {
        var favouriteArticle = await _context.FavouriteArticles.FindAsync(id);

        if (favouriteArticle == null)
        {
            return null;
        }

        _context.FavouriteArticles.Remove(favouriteArticle);
        await _context.SaveChangesAsync();
        return favouriteArticle;
    }
}