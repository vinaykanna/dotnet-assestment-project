
using NewsApi.DTOs;
using NewsApi.Models;
using NewsApi.Repositories;

namespace NewsApi.Services;

public interface IArticlesService
{
    Task<List<ArticleResponseDto>> GetArticles();
    Task<ArticleResponseDto> CreateArticle(ArticleDto articleDto, string authorId);
}

public class ArticlesService(IArticleRepository productsRepository) : IArticlesService
{

    private readonly IArticleRepository _articleRepository = productsRepository;

    public async Task<List<ArticleResponseDto>> GetArticles()
    {
        var articles = await _articleRepository.GetAllAsync();

        return articles.Select(a => new ArticleResponseDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content!,
            Status = a.Status,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            Author = new UserResponseDto
            {
                Id = a.Author!.Id!,
                Name = a.Author.Name,
            }
        }).ToList();
    }

    public async Task<ArticleResponseDto> CreateArticle(ArticleDto articleDto, string authorId)
    {
        var newArticle = new Article
        {
            Title = articleDto.Title,
            Content = articleDto.Content,
            AuthorId = Guid.Parse(authorId!),
            Status = articleDto.Status,
            CreatedAt = DateTime.UtcNow
        };

        var article = await _articleRepository.CreateAsync(newArticle);

        return new ArticleResponseDto
        {
            Id = article!.Id,
            Title = article.Title,
            Content = article.Content!,
            Status = article.Status,
            CreatedAt = article.CreatedAt
        };
    }
}