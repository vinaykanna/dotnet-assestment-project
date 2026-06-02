using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.DTOs;
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

    public async Task<List<Article>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(a => a.Author)
            .OrderByDescending(a => a.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Articles.CountAsync();
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

    public async Task<Article?> UpdateAsync(Article article)
    {
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<List<TrendingArticleDto>> GetTrendingArticlesAsync()
    {
        return await _context.Set<TrendingArticleDto>()
            .FromSqlRaw(@"
             SELECT TOP 10
                a.Id,
                a.Title,
                ISNULL(v.ViewCount, 0) AS ViewCount,
                ISNULL(c.CommentCount, 0) AS CommentCount,
                (
                    ISNULL(v.ViewCount, 0) +
                    ISNULL(c.CommentCount, 0) * 3
                ) AS TrendingScore
                FROM Articles a
                LEFT JOIN
                (
                    SELECT
                        ArticleId,
                        COUNT(*) AS ViewCount
                    FROM ArticleViews
                    WHERE ViewedAt >= DATEADD(DAY, -7, GETUTCDATE())
                    GROUP BY ArticleId
                ) v ON a.Id = v.ArticleId
                LEFT JOIN
                (
                    SELECT
                        ArticleId,
                        COUNT(*) AS CommentCount
                    FROM Comments
                    WHERE CreatedAt >= DATEADD(DAY, -7, GETUTCDATE())
                    GROUP BY ArticleId
                ) c ON a.Id = c.ArticleId
             ORDER BY TrendingScore DESC")
            .AsNoTracking()
            .ToListAsync();
    }
}