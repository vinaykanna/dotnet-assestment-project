namespace NewsApi.Models;

public class FavouriteArticle
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid ArticleId { get; set; }
    public Article? Article { get; set; }
    public DateTime CreatedAt { get; set; }
}