using NewsApi.DTOs;
using NewsApi.Exceptions;
using NewsApi.Models;
using NewsApi.Repositories;

namespace NewsApi.Services;

public interface IFavouriteArticlesService
{
    Task<List<FavouriteArticle>> GetFavourites();
    Task<FavouriteArticle?> CreateFavourite(FavouriteArticleDto favouriteArticle, Guid currentUserId);
    Task<FavouriteArticle?> DeleteFavourite(Guid id, Guid currentUserId);
}

public class FavouriteArticlesService(IFavouriteArticleRepository favouriteArticleRepository) : IFavouriteArticlesService
{
    public async Task<List<FavouriteArticle>> GetFavourites()
    {
        return await favouriteArticleRepository.GetAllAsync();
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