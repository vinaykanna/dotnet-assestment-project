using System.ComponentModel.DataAnnotations;
using NewsApi.Enums;

namespace NewsApi.DTOs;

public class FavouriteArticleDto
{
    [Required]
    public required Guid ArticleId { get; set; }
}