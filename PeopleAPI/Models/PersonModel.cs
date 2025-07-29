namespace PeopleAPI.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Comments { get; set; }
        
        // Navigation properties for relationships
        public int? ProfessionId { get; set; } // Foreign key to Profession
        public ProfessionModel? Profession { get; set; } // Navigation property to single Profession
        
        public ICollection<HobbyModel> Hobbies { get; set; } = new List<HobbyModel>(); // Navigation property to multiple Hobbies
    }
}