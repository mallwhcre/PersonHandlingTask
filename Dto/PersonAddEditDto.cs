using System.ComponentModel.DataAnnotations;
using PeopleAPI.Common;

namespace PeopleAPI.Dto
{
    public class PersonAddEditDto //remove ID
    {   
        //public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain only letters.")]
        [Required (ErrorMessage = "First Name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain only letters.")]
        [Required (ErrorMessage = "Last Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required (ErrorMessage = "Date of Birth is required")]
        [ValidDateDOB]
        public DateOnly DateOfBirth { get; set; }
        
        [StringLength(500)]
        public string? Comments { get; set; }
        
        public int? ProfessionId { get; set; }
        
        public List<int> HobbyIds { get; set; } = new();
    }
}