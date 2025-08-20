namespace PeopleAPI.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Comments { get; set; }
        public int? ProfessionId { get; set; }
    
        public List<HobbyModel> Hobbies { get; set; } = new(); //arr of hobbies person has
    }
}