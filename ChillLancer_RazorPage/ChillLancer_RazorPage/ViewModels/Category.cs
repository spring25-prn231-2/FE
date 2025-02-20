using System.ComponentModel.DataAnnotations;

namespace ChillLancer_RazorPage.ViewModels
{
    public class Category
    {
        public string? Id { get; set; }
        public string? MajorName { get; set; }
        public string? BriefName { get; set; }
        public string? SpecializedName { get; set; }
        public string? Status { get; set; }
    }
}
