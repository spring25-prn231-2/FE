namespace ChillLancer_RazorPage.Models.ViewModels
{
    public class GroupCategoryVM
    {
        public string BriefName { get; set; }
        public List<CategoryVM> SpecializedCategories { get; set; } = new List<CategoryVM>();
    }
}
