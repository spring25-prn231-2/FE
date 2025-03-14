using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models.ViewModels
{
    public class CategoryVM
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("major-name")]
        public string MajorName { get; set; } = null!;

        [JsonPropertyName("brief-name")]
        public string BriefName { get; set; } = null!;

        [JsonPropertyName("specialized-name")]
        public string SpecializedName { get; set; } = null!;

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Created";
    }
}
