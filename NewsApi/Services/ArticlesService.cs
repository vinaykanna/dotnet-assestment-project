
using NewsApi.DTOs;
using NewsApi.Exceptions;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services.Interfaces;

namespace NewsApi.Services;

public class ArticlesService(IArticleRepository articleRepository) : IArticlesService
{
    public async Task<PagedResponse<ArticleResponseDto>> GetArticles(
     int pageNumber,
     int pageSize)
    {
        var articles = await articleRepository
            .GetAllAsync(pageNumber, pageSize);

        var totalCount = await articleRepository
            .GetCountAsync();

        return new PagedResponse<ArticleResponseDto>
        {
            Items = articles.Select(a => new ArticleResponseDto
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
                    Email = a.Author.Email
                }
            }).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
                totalCount / (double)pageSize)
        };
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

        var article = await articleRepository.CreateAsync(newArticle);

        return new ArticleResponseDto
        {
            Id = article!.Id,
            Title = article.Title,
            Content = article.Content!,
            Status = article.Status,
            CreatedAt = article.CreatedAt
        };
    }

    public async Task<ArticleResponseDto> UpdateArticle(
        Guid articleId,
        UpdateArticleDto request,
        Guid currentUserId)
    {
        var article = await articleRepository.GetAsync(articleId);

        if (article == null)
        {
            throw new NotFoundException("Article", articleId.ToString());
        }

        if (article.AuthorId != currentUserId)
        {
            throw new ForbiddenException("You can only update your own articles.");
        }

        article.Title = request.Title;
        article.Content = request.Content;

        await articleRepository.UpdateAsync(article);

        return new ArticleResponseDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content!,
            Status = article.Status,
            CreatedAt = article.CreatedAt
        };
    }

    public async Task DeleteArticle(
    Guid articleId,
    Guid currentUserId)
    {
        var article = await articleRepository.GetAsync(articleId);

        if (article == null)
        {
            throw new NotFoundException("Article", articleId.ToString());
        }

        if (article.AuthorId != currentUserId)
        {
            throw new ForbiddenException("You can only delete your own articles.");
        }

        await articleRepository.DeleteAsync(articleId);
    }
}