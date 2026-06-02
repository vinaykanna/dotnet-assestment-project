
using NewsApi.Models;
using NewsApi.Repositories;

namespace NewsApi.Services;

public interface IArticlesService
{
    Task<List<Article>> GetArticles();
}

public class ArticlesService : IArticlesService
{

    private readonly IArticleRepository _articleRepository;

    private readonly ILogger<ArticlesService> _logger;
    public ArticlesService(IArticleRepository productsRepository, ILogger<ArticlesService> logger)
    {
        _articleRepository = productsRepository;
        _logger = logger;
    }

    public async Task<List<Article>> GetArticles()
    {
        var articles = await _articleRepository.GetAllAsync();

        return articles;
    }
}