using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models.ViewModels
{
    public class LanguageVM
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;

        [JsonPropertyName("proficiency-level")]
        [MaxLength(30)]
        public string? ProficiencyLevel { get; set; }

        [JsonPropertyName("status")]
        [MaxLength(30)]
        public string Status { get; set; } = "Created";

        [JsonPropertyName("account-id")]
        public Guid AccountId { get; set; } = Guid.Empty;
    }
}
