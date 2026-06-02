
using NewsApi.Enums;

namespace NewsApi.Models;

public class Article
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public Guid AuthorId { get; set; }
    public User? Author { get; set; }
    public ArticleStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IList<Comment>? Comments { get; set; }

    public IList<ArticleView>? ArticleViews { get; set; }
}