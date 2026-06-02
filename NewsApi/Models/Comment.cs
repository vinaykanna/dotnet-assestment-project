namespace NewsApi.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Article? Article { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}