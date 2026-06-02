
namespace NewsApi.DTOs;

public class TrendingArticleDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int ViewCount { get; set; }

    public int CommentCount { get; set; }

    public int TrendingScore { get; set; }
}