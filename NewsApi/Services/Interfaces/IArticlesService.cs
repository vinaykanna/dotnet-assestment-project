using NewsApi.DTOs;

namespace NewsApi.Services.Interfaces;

public interface IArticlesService
{
    Task<PagedResponse<ArticleResponseDto>> GetArticles(int pageNumber, int pageSize);
    Task<ArticleResponseDto> CreateArticle(ArticleDto articleDto, string authorId);

    Task<ArticleResponseDto> UpdateArticle(
        Guid articleId,
        UpdateArticleDto request,
        Guid currentUserId);

    Task DeleteArticle(Guid articleId, Guid currentUserId);
}