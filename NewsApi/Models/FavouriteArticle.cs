
namespace NewsApi.Models;

public class FavoriteArticle
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }

    public Guid ArticleId { get; set; }
    public required Article Article { get; set; }
}