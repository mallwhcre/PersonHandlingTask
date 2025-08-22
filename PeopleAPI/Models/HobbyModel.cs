using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PeopleAPI.Models
{
    public class HobbyModel
    {
        public int Id { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
    }
}