
using NewsApi.Enums;

namespace NewsApi.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public IList<Article>? AuthoredArticles { get; set; }
    public IList<FavouriteArticle>? FavouritedArticles { get; set; }
}