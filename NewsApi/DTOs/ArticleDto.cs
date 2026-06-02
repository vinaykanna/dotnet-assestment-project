using System.ComponentModel.DataAnnotations;
using NewsApi.Enums;

namespace NewsApi.DTOs;

public class ArticleDto
{
    [Required]
    [MinLength(3)]
    public required string Title { get; set; }

    [Required]
    public required string Content { get; set; }

    [Required]
    public ArticleStatus Status { get; set; }
}