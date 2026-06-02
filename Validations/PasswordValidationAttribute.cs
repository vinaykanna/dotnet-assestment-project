using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is not string password)
        {
            return new ValidationResult("Password is required.");
        }

        if (password.Length < 8)
        {
            return new ValidationResult("Password must be at least 8 characters long.");
        }

        if (!Regex.IsMatch(password, @"[A-Za-z]"))
        {
            return new ValidationResult("Password must contain at least one letter.");
        }

        if (!Regex.IsMatch(password, @"\d"))
        {
            return new ValidationResult("Password must contain at least one number.");
        }

        return ValidationResult.Success;
    }
}