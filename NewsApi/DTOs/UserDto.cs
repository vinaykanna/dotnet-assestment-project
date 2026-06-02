using System.ComponentModel.DataAnnotations;

namespace NewsApi.DTOs;

public class UserDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    [PasswordValidation]
    public string Password { get; set; }

    [Required]
    public bool IsAuthor { get; set; }
}