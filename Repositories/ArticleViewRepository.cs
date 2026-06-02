
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;

namespace NewsApi.Repositories;

public class ArticleViewRepository(AppDbContext context) : IArticleViewRepository
{
    private readonly AppDbContext _context = context;

    public async Task<ArticleView?> AddArticleView(ArticleView articleView)
    {
        await _context.ArticleViews.AddAsync(articleView);
        await _context.SaveChangesAsync();
        return articleView;
    }
}