using NewsApi.DTOs;

namespace NewsApi.Services.Interfaces;

public interface ITrendingArticlesService
{
    Task<List<TrendingArticleDto>> GetTrendingArticles();

    Task RefreshTrendingArticles();
}