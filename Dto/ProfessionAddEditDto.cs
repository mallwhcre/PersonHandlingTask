using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class ProfessionAddEditDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Profession Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}