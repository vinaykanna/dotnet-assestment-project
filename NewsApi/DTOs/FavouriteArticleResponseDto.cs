using System.ComponentModel.DataAnnotations;

namespace NewsApi.DTOs;

public class FavouriteArticleResponseDto
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required Guid ArticleId { get; set; }

    [Required]
    public required string ArticleTitle { get; set; }

    [Required]
    public required string ArticleContent { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }
}