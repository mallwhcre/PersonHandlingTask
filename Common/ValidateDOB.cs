using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Common
{
    public class ValidDateDOBAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateOnly dateOfBirth)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                
                //not future dob
                if (dateOfBirth > today)
                {
                    ErrorMessage = "Date of Birth cannot be in the future.";
                    return false;
                }
        
                var age = today.Year - dateOfBirth.Year;
        
                if (age > 120)
                {
                    ErrorMessage = "Date of Birth cannot be more than 120 years ago.";
                    return false;
                }
                
                return true;
            }
            
            return false;
        }
    }
}