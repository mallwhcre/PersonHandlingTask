using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class HobbyAddEditDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Hobby Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}