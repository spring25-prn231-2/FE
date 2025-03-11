using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Model.AccountDtos
{
    public class AccountCreateDto
    {
        [JsonPropertyName("avatar-url")]
        public string? AvatarUrl { get; set; }
        [JsonPropertyName("firebase-uid")]
        public string? FirebaseUid { get; set; } = "string";
        [JsonPropertyName("fcm-token")]
        public string? FcmToken { get; set; } = "string";
        [JsonPropertyName("google-id")]
        public string? GoogleId { get; set; } = "string";
        [JsonPropertyName("facebook-id")]
        public string? FacebookId { get; set; } = "string";
        [JsonPropertyName("name-tag")]
        public string NameTag { get; set; } = "string";
        [JsonPropertyName("identity-code")]
        public string? IdentityCode { get; set; } = "string";
        [JsonPropertyName("full-name")]
        public string FullName { get; set; } = null!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("phone-number")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("date-of-birth")]
        public DateTime? DateOfBirth { get; set; }
        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("national")]
        public string? National { get; set; }
        [JsonPropertyName("bio-summary")]
        public string? BioSummary { get; set; }
        [JsonPropertyName("company-name")]
        public string? CompanyName { get; set; }
        [JsonPropertyName("company-website")]
        public string? CompanyWebsite { get; set; } = "string";
        [JsonPropertyName("favorited")]
        public int Favorited { get; set; }
        [JsonPropertyName("liked")]
        public int Liked { get; set; }
        [JsonPropertyName("dislike")]
        public int Dislike { get; set; }
        [JsonPropertyName("is-verified")]
        public bool IsVerified { get; set; }
        [JsonPropertyName("last-delivery")]
        public DateTime? LastDelivery { get; set; }
        [JsonPropertyName("joined-date")]
        public DateTime JoinedDate { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;
        [JsonPropertyName("status")]
        public string Status { get; set; } = "Created";
    }
}
