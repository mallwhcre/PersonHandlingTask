using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Dto
{
    public class ProfessionAddEditDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}