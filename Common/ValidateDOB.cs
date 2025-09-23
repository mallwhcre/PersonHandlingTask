using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Common
{
    public class ValidDateOfBirthAttribute : ValidationAttribute
    {
        public ValidDateOfBirthAttribute()
        {
            ErrorMessage = "Invalid date of birth. Date must be in the past and represent a realistic age.";
        }

        public override bool IsValid(object? value)
        {
            if (value is DateOnly dateOfBirth)
            {
                return InputGuards.IsValidAge(dateOfBirth);
            }
            return false;
        }
    }
}