using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class HobbyAddEditDto
    {
        //public int Id { get; set; } //ToDo: Remove ID
        [Required (ErrorMessage = "Hobby Name is required")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Hobby Name must contain only letters.")]
        public string Name { get; set; }
    }
}