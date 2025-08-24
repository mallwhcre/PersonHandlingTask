namespace PeopleAPI.Dto
{
    public class PersonViewDto
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Comments { get; set; }
       // public int? ProfessionId { get; set; }
        public string? ProfessionName { get; set; }
        public List<HobbyViewDto> Hobbies { get; set; } = new();
    }
}