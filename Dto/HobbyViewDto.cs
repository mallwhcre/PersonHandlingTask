using System.Text.Json.Serialization;

namespace PeopleAPI.Dto
{
    public class HobbyViewDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}