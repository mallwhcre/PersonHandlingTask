namespace PeopleAPI.Models
{
    public class HobbyModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        
        // Navigation properties
        public List<PersonModel> People { get; set; } = new();
    }
}