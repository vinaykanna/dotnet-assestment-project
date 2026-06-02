using System.ComponentModel.DataAnnotations;

namespace NewsApi.DTOs;

public class ProductDto
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }
}