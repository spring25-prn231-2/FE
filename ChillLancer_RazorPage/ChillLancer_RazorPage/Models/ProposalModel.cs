using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models
{
    public class ProposalModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(50)]
        [JsonPropertyName("freelancer-name")]
        public string FreelancerName { get; set; } = null!;
        [Required, MaxLength(255)]
        public string Title { get; set; } = null!;
        [Required, MaxLength(1000)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        [JsonPropertyName("hour-duration")]
        public int HourDuration { get; set; }
        [JsonPropertyName("delivery-time")]
        public int DeliveryTime { get; set; } = 1;
        [JsonPropertyName("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonPropertyName("applied-date")]
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        [MaxLength(30)]
        public string Status { get; set; } = "Created";
        [JsonPropertyName("project-id")]
        public Guid ProjectId { get; set; } = Guid.Empty;
        [JsonPropertyName("account-id")]
        public Guid AccountId { get; set; }
        [JsonPropertyName("processes")]
        public List<MilestoneModel> Processes { get; set; } = [];
    }
}
