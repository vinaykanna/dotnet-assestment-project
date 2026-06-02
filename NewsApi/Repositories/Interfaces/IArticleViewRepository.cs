

using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface IArticleViewRepository
{
    Task<ArticleView?> AddArticleView(ArticleView articleView);
}