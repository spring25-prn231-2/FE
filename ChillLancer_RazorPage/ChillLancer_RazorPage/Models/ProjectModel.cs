using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models
{
    public class ProjectModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Guidelines { get; set; }
        public decimal Budget { get; set; } = 0;
        public int Duration { get; set; } = 1;//How many days to work from start date
        [JsonPropertyName("start-date")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("end-date")]
        public DateTime? EndDate { get; set; }
        public string? RequirementNote { get; set; }
    }
}
