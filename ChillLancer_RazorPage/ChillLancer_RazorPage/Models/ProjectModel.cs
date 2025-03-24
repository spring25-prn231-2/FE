using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models
{
    public class ProjectModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("guidelines")]
        public string Guidelines { get; set; }

        [JsonPropertyName("budget")]
        public decimal Budget { get; set; }

        [JsonPropertyName("start-date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end-date")]
        public DateTime EndDate { get; set; }
        public int Duration { get; set; } = 1;

        [JsonPropertyName("requirement-note")]
        public string RequirementNote { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("employer-id")]
        public Guid? EmployerId { get; set; }

        [JsonPropertyName("category-id")]
        public Guid? CategoryId { get; set; }

        [JsonPropertyName("skill-ids")]
        public List<string>? SkillIds { get; set; }
        public string CategoryName { get; set; }
        public List<string>? SkillName { get; set; }
    }
    public class ProjectCreateModel
    {
        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("guidelines")]
        public string? Guidelines { get; set; }
        [JsonPropertyName("budget")]
        [Required]
        public decimal Budget { get; set; } = 0;
        [JsonPropertyName("start-date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end-date")]
        public DateTime? EndDate { get; set; }
        public int Duration { get; set; } = 1;
        [JsonPropertyName("requirement-note")]
        public string? RequirementNote { get; set; }
        [Required]
        [JsonPropertyName("category-id")]
        public Guid categoryId { get; set; }
        [JsonPropertyName("skill-ids")]
        public ICollection<Guid>? skillIds { get; set; }
    }
}
