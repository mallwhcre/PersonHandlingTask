namespace PeopleAPI.Models
{
    public class ProfessionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Navigation property - one profession can have many people
        public ICollection<PersonModel> People { get; set; } = new List<PersonModel>();
    }
}