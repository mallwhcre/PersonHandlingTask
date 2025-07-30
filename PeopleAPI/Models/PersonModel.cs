namespace PeopleAPI.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Comments { get; set; }
        public int? ProfessionId { get; set; }
    }
}