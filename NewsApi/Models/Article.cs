
using NewsApi.Enums;

namespace NewsApi.Models;

public class Article
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid AuthorId { get; set; }
    public required User Author { get; set; }

    public required ArticleStatus Status { get; set; }
    public IList<FavoriteArticle>? FavoritedBy { get; set; }
}