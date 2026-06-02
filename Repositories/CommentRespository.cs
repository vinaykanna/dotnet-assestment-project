
using Microsoft.EntityFrameworkCore;
using NewsApi.Data;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;

namespace NewsApi.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Comment?> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<Comment>> GetByArticleIdAsync(Guid articleId)
    {
        return await _context.Comments
                .AsNoTracking()
                .Where(x => x.ArticleId == articleId)
                .Include(x => x.Article)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid commentId)
    {
        var comment = await _context.Comments
        .Include(c => c.User)
        .FirstOrDefaultAsync(c => c.Id == commentId);

        return comment;
    }

    public async Task<Comment?> DeleteAsync(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}