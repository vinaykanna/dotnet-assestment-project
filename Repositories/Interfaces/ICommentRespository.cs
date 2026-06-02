
using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetByArticleIdAsync(Guid articleId);
    Task<Comment?> GetByIdAsync(Guid commentId);
    Task<Comment?> CreateAsync(Comment comment);
    Task<Comment?> DeleteAsync(Comment comment);
}