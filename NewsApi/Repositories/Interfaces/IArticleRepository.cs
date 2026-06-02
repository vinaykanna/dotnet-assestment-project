
using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface IArticleRepository
{
    Task<List<Article>> GetAllAsync(int pageNumber, int pageSize);

    Task<int> GetCountAsync();
    Task<Article?> CreateAsync(Article produdct);
    Task<Article?> DeleteAsync(Guid id);
    Task<Article?> UpdateAsync(Article product);
    Task<Article?> GetAsync(Guid id);
}