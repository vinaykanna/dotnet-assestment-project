using NewsApi.DTOs;
using NewsApi.Exceptions;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services.Interfaces;
namespace NewsApi.Services;

public class FavouriteArticlesService(IFavouriteArticleRepository favouriteArticleRepository) : IFavouriteArticlesService
{
    public async Task<List<FavouriteArticleResponseDto>> GetFavourites()
    {
        var articles = await favouriteArticleRepository.GetAllAsync();

        return articles.Select(a => new FavouriteArticleResponseDto
        {
            Id = a.Id,
            ArticleId = a.Article!.Id,
            ArticleTitle = a.Article!.Title,
            ArticleContent = a.Article!.Content!,
            CreatedAt = a.CreatedAt,
        }).ToList();
    }

    public async Task<FavouriteArticle?> CreateFavourite(FavouriteArticleDto favouriteArticle, Guid currentUserId)
    {
        var newFavouriteArticle = new FavouriteArticle
        {
            ArticleId = favouriteArticle.ArticleId,
            UserId = currentUserId,
            CreatedAt = DateTime.UtcNow
        };

        return await favouriteArticleRepository.CreateAsync(newFavouriteArticle);
    }

    public async Task<FavouriteArticle?> DeleteFavourite(Guid id, Guid currentUserId)
    {
        var favouriteArticle = await favouriteArticleRepository.DeleteAsync(id);

        if (favouriteArticle == null)
        {
            throw new NotFoundException("Favourite article", id.ToString());
        }

        if (favouriteArticle.UserId != currentUserId)
        {
            throw new ForbiddenException("You can only delete your own favourite articles.");
        }

        return favouriteArticle;
    }
}