using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class PersonAddEditDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        public DateOnly DateOfBirth { get; set; }
        
        [StringLength(500)]
        public string? Comments { get; set; }
        
        public int? ProfessionId { get; set; }
        
        public List<int> HobbyIds { get; set; } = new();
    }
}