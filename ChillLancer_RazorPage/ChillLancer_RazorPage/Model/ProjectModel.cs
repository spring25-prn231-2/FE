using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Model
{
    public class ProjectModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
