using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Admin.Management
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

        [BindProperty]
        public CategoryVM Category { get; set; } = new CategoryVM();

        [BindProperty]
        public bool IsEdit { get; set; } = false;

        //==========================================================================================
        public async Task OnGetAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/category/list?PageSize=999";

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
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/category";

            if (!IsEdit)
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, Category);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Add New Failed!");
                    await OnGetAsync();
                    return Page();
                }
            }
            else
            {
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), apiUrl)
                {
                    Content = JsonContent.Create(Category)
                };
                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Update Failed!");
                    await OnGetAsync();
                    return Page();
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/category";

            var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");
            var message = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Delete Failed!");
            }
            return RedirectToPage();
        }
    }
}
