using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class PersonAddEditDto //remove ID
    {   
        public int Id { get; set; }
        [Required (ErrorMessage = "First Name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required (ErrorMessage = "Last Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required (ErrorMessage = "Date of Birth is required")]
        public DateOnly DateOfBirth { get; set; }
        
        [StringLength(500)]
        public string? Comments { get; set; }
        
        public int? ProfessionId { get; set; }
        
        public List<int> HobbyIds { get; set; } = new();
    }
}