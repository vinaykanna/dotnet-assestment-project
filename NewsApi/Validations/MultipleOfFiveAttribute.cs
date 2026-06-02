using System.ComponentModel.DataAnnotations;

namespace NewsApi.Validations;

public class MultipleOfFiveAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not int)
        {
            return new ValidationResult("Price should be a number");
        }


        if (value is int price)
        {
            if (price % 5 != 0)
            {
                return new ValidationResult("Price must be multiple of 5");
            }
        }

        return ValidationResult.Success;
    }
}