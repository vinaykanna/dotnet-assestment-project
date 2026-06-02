using NewsApi.DTOs;
using NewsApi.Models;

namespace NewsApi.Services.Interfaces;

public interface IFavouriteArticlesService
{
    Task<List<FavouriteArticleResponseDto>> GetFavourites();
    Task<FavouriteArticle?> CreateFavourite(FavouriteArticleDto favouriteArticle, Guid currentUserId);
    Task<FavouriteArticle?> DeleteFavourite(Guid id, Guid currentUserId);
}