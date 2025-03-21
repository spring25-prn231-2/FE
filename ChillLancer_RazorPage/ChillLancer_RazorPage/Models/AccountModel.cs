using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Transactions;

namespace ChillLancer_RazorPage.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [JsonPropertyName("avatar-url")]
        public string? AvatarUrl { get; set; }
        [JsonPropertyName("firebase-uid")]
        public string? FirebaseUid { get; set; }
        [JsonPropertyName("fcm-token")]
        public string? FcmToken { get; set; }
        [JsonPropertyName("google-id")]
        public string? GoogleId { get; set; }
        [JsonPropertyName("facebook-id")]
        public string? FacebookId { get; set; }

        [Required, MaxLength(100), JsonPropertyName("name-tag")]
        public string NameTag { get; set; } = null!;

        [MaxLength(30), JsonPropertyName("identity-code")]
        public string? IdentityCode { get; set; }

        [Required, MaxLength(50), JsonPropertyName("full-name")]
        public string FullName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [MaxLength(15), JsonPropertyName("phone-number")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [JsonPropertyName("date-of-birth")]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(20)]
        public string? Gender { get; set; }
        [MaxLength(100)]
        public string? National { get; set; }
        [JsonPropertyName("bio-summary")]
        public string? BioSummary { get; set; }
        [MaxLength(200), JsonPropertyName("company-name")]
        public string? CompanyName { get; set; }
        [MaxLength(255), JsonPropertyName("company-website")]
        public string? CompanyWebsite { get; set; }
        public int Favorited { get; set; } = 0;
        public int Liked { get; set; } = 0;
        public int Dislike { get; set; } = 0;
        [JsonPropertyName("is-verified")]
        public bool IsVerified { get; set; } = false;
        [JsonPropertyName("last-delivery")]
        public DateTime? LastDelivery { get; set; }
        [JsonPropertyName("joined-date")]
        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
        [MaxLength(30)]
        public string Role { get; set; } = "Customer";
        [MaxLength(30)]
        public string Status { get; set; } = "Created";

        //======================[ Foreign Key ]======================
        //public virtual ICollection<Certification>? Certifications { get; set; }
        //public virtual ICollection<AccountLanguage>? AccountLanguages { get; set; }
        //public virtual ICollection<Education>? Educations { get; set; }
        //public virtual ICollection<Proposal>? Proposals { get; set; }
        //public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<Transaction>? TransactionsAsFreelancer { get; set; }
        public virtual ICollection<Transaction>? TransactionsAsEmployer { get; set; }

        //public virtual ICollection<ProjectContract>? ProjectContracts { get; set; }
        //public virtual ICollection<PersonalContract>? PersonalContracts { get; set; }
    }
}
