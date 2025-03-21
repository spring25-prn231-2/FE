using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models
{
    public class SkillModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("level")]
        public string? Level { get; set; }
    }
}
