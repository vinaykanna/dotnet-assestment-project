using NewsApi.Enums;
using NewsApi.Models;

namespace NewsApi.DTOs;

public class ArticleResponseDto
{

    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Content { get; set; }

    public UserResponseDto Author { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ArticleStatus Status { get; set; }
}