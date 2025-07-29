namespace PeopleAPI.Models
{
    public class HobbyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Navigation property - one hobby can be linked to many people (many-to-many relationship)
        public ICollection<PersonModel> People { get; set; } = new List<PersonModel>();
    }
}