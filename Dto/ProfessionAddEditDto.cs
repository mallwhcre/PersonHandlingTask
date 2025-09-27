using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class ProfessionAddEditDto
    {
    
        [Required (ErrorMessage = "Profession Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Profession Name must contain only letters.")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}