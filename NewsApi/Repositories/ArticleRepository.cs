
using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;

namespace NewsApi.Repositories;

public class ArticlesRepository(AppDbContext context) : IArticleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Article?> CreateAsync(Article product)
    {
        await _context.Articles.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Article>> GetAllAsync()
    {
        return await _context.Articles
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Author)
                .ToListAsync();
    }

    public async Task<Article?> GetAsync(Guid id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task<Article?> DeleteAsync(Guid id)
    {
        var Article = await _context.Articles.FindAsync(id);

        if (Article == null)
        {
            return null;
        }

        _context.Articles.Remove(Article);
        await _context.SaveChangesAsync();

        return Article;
    }

    public async Task<Article?> UpdateAsync(Article product)
    {
        _context.Articles.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}