using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models.ViewModels
{
    public class AccountVM
    {
        [JsonPropertyName("id")] public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("avatar-url")] public string? AvatarUrl { get; set; }
        [JsonPropertyName("firebase-uid")] public string? FirebaseUid { get; set; }
        [JsonPropertyName("fcm-token")] public string? FcmToken { get; set; }
        [JsonPropertyName("google-id")] public string? GoogleId { get; set; }
        [JsonPropertyName("facebook-id")] public string? FacebookId { get; set; }

        [JsonPropertyName("name-tag")]
        [Required, MaxLength(100)]
        public string NameTag { get; set; } = null!;

        [JsonPropertyName("identity-code")]
        [MaxLength(30)]
        public string? IdentityCode { get; set; }

        [JsonPropertyName("full-name")]
        [Required, MaxLength(50)]
        public string FullName { get; set; } = null!;

        [JsonPropertyName("email")]
        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; } = null!;

        [JsonPropertyName("phone-number")]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("address")] public string? Address { get; set; }
        [JsonPropertyName("date-of-birth")] public DateTime? DateOfBirth { get; set; }

        [JsonPropertyName("gender")]
        [MaxLength(20)]
        public string? Gender { get; set; }

        [JsonPropertyName("national")]
        [MaxLength(100)]
        public string? National { get; set; }
        [JsonPropertyName("bio-summary")] public string? BioSummary { get; set; }

        [JsonPropertyName("company-name")]
        [MaxLength(200)]
        public string? CompanyName { get; set; }

        [JsonPropertyName("company-website")]
        [MaxLength(255)]
        public string? CompanyWebsite { get; set; }
        [JsonPropertyName("favorited")] public int Favorited { get; set; } = 0;
        [JsonPropertyName("liked")] public int Liked { get; set; } = 0;
        [JsonPropertyName("dislike")] public int Dislike { get; set; } = 0;
        [JsonPropertyName("is-verified")] public bool IsVerified { get; set; } = false;
        [JsonPropertyName("last-delivery")] public DateTime? LastDelivery { get; set; }
        [JsonPropertyName("joined-date")] public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("role")]
        [MaxLength(30)]
        public string Role { get; set; } = "Customer";

        [JsonPropertyName("status")]
        [MaxLength(30)]
        public string Status { get; set; } = "Created";
    }
}
