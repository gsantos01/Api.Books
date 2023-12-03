using System.ComponentModel.DataAnnotations;

namespace Api.Books.Api.Attributes
{
    public class DateOfBirthNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date > DateTime.Now.AddYears(5))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
