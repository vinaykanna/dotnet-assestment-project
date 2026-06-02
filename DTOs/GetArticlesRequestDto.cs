using System.ComponentModel.DataAnnotations;

namespace NewsApi.DTOs;

public class GetArticlesRequestDto
{
    [Required]
    public int? PageNumber { get; set; }

    [Required]
    public int? PageSize { get; set; }
}