using System.Text.Json.Serialization;

namespace PeopleAPI.Dto
{
    public class HobbyViewDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] //ignores id=0
        public int Id { get; set; }
        public string Name { get; set; }
    }
}