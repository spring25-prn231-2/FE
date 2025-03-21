using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models.ViewModels
{
    public class SkillVM
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status")]
        [MaxLength(30)]
        public string Status { get; set; } = "Created";

        [JsonPropertyName("level")]
        [MaxLength(30)]
        public string? Level { get; set; }

        [JsonPropertyName("project-id")]
        public Guid ProjectId { get; set; } = Guid.Empty;

        [JsonPropertyName("proposal-id")]
        public Guid ProposalId { get; set; } = Guid.Empty;
    }
}
