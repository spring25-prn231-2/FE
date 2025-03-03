﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Model
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
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int HourDuration { get; set; }
        public int DeliveryTime { get; set; } = 1;
        [JsonPropertyName("created-at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(30)]
        public string Status { get; set; } = "Created";
        [JsonPropertyName("project-id")]
        public Guid ProjectId { get; set; } = Guid.Empty;
        [JsonPropertyName("account-id")]
        public Guid AccountId { get; set; }
        //public List<ProcessBM> Processes { get; set; } = [];
    }
}
