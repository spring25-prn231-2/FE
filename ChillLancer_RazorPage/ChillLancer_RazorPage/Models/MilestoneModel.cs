namespace ChillLancer_RazorPage.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class MilestoneModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [JsonPropertyName("task-name")]
    public string TaskName { get; set; } = null!;
    [JsonPropertyName("task-description")]
    public string? TaskDescription { get; set; }
    [JsonPropertyName("start-date")]
    public DateTime? StartDate { get; set; }
    [JsonPropertyName("end-date")]
    public DateTime? EndDate { get; set; }
    public decimal Cost { get; set; } = 0;
    [JsonPropertyName("is-paid")]
    public bool IsPaid { get; set; } = false;
    public string? Note { get; set; }
    public string Status { get; set; } = "Draft";
}
