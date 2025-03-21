using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CategoriesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public List<CategoryVM> Categories { get; set; } = new List<CategoryVM>();
        public List<List<GroupCategoryVM>> ColumnCategories { get; set; } = new List<List<GroupCategoryVM>>();

        public List<GroupCategoryVM> GroupedCategories { get; set; } = new List<GroupCategoryVM>();


        //==========================================================================================
        public async Task<IActionResult> OnGetAsync(string majorName)
        {
            if (string.IsNullOrEmpty(majorName))
            {
                return Page();
            }

            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/category/list?KeyWord={Uri.EscapeDataString(majorName)}&InField=MN&PageSize=999";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ListResponse<CategoryVM>>(apiUrl);
                if (response != null && response.DataList != null)
                {
                    Categories = response.DataList;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Fetch categories error: " + ex.Message);
            }

            var grouped = Categories
                .GroupBy(c => c.BriefName)
                .Select(g => new GroupCategoryVM
                {
                    BriefName = g.Key,
                    SpecializedCategories = g.ToList()
                })
                .OrderBy(x => x.BriefName)
                .ToList();

            GroupedCategories = grouped;

            ViewData["MajorName"] = majorName;
            return Page();
        }
    }
}
