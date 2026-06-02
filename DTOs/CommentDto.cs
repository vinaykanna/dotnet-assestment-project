
using System.ComponentModel.DataAnnotations;

namespace NewsApi.DTOs;

public class CommentDto
{
    [Required]
    [NotEmptyGuid]
    public Guid ArticleId { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; } = string.Empty;
}

