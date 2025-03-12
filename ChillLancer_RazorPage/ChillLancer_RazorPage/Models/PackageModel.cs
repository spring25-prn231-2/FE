using System.ComponentModel.DataAnnotations;

namespace ChillLancer_RazorPage.Models
{
    public class PackageModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(20)]
        public string Code { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int DayDuration = 0;
        [MaxLength(30)]
        public string Status { get; set; } = "Active";
    }
}
