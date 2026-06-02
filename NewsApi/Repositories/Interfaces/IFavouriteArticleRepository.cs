
using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface IFavouriteArticleRepository
{
    Task<List<FavouriteArticle>> GetAllAsync();
    Task<FavouriteArticle?> CreateAsync(FavouriteArticle favouriteArticle);
    Task<FavouriteArticle?> GetAsync(Guid id);
    Task<FavouriteArticle?> DeleteAsync(FavouriteArticle favouriteArticle);
}