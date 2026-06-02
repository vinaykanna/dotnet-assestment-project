using System.ComponentModel.DataAnnotations;
using NewsApi.Enums;

namespace NewsApi.DTOs;

public class UpdateArticleDto
{
    [Required]
    [MinLength(3)]
    public required string Title { get; set; }

    [Required]
    public required string Content { get; set; }

    [Required]
    public required ArticleStatus Status { get; set; }
}