namespace NewsApi.DTOs;

public class CommentResponseDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}