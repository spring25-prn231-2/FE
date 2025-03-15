using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models.ViewModels
{
    public class CodeVM
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("code")]
        [Required, MaxLength(50)]
        public string Code { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("type")]
        [Required, MaxLength(30)]
        public string Type { get; set; } = "Promotion";//Operating Fee

        [JsonPropertyName("percentage")]
        public decimal Percentage { get; set; } = 0;// 99.99d = 99.99  %

        [JsonPropertyName("status")]
        [MaxLength(30)]
        public string Status { get; set; } = "Active";
    }
}
