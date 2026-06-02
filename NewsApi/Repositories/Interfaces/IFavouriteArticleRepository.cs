
using NewsApi.Models;

namespace NewsApi.Repositories.Interfaces;

public interface IFavouriteArticleRepository
{
    Task<List<FavouriteArticle>> GetAllAsync();

    Task<FavouriteArticle?> CreateAsync(FavouriteArticle produdct);

    Task<FavouriteArticle?> DeleteAsync(Guid id);
}