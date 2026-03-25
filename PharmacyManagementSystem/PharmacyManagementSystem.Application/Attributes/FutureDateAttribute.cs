using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Application.Attributes
{
    /// <summary>
    /// Validates that a date is in the future.
    /// </summary>
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "The {0} must be a future date.";
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return false;
        }
    }
}
