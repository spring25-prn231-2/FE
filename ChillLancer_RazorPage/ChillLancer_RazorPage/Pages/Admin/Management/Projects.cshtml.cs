using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Admin.Management
{
    public class ProjectsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProjectsModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public List<ProjectVM> ProjectsList { get; set; } = [];

        [BindProperty]
        public ProjectVM ProjectObj { get; set; } = new ProjectVM();

        [BindProperty]
        public bool IsEdit { get; set; } = false;

        //==========================================================================================
        public async Task OnGetAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/project/list?PageSize=999&status=exceptdeleted";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ListResponse<ProjectVM>>(apiUrl);
                if (response != null && response.DataList != null)
                {
                    ProjectsList = response.DataList;
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
            var apiUrl = $"{serverUrl}/api/project";

            if (!IsEdit)
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, ProjectObj);
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
                    Content = JsonContent.Create(ProjectObj)
                };
                var response = await _httpClient.SendAsync(request);
                var message = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, message);
                    await OnGetAsync();
                    return Page();
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/project";

            var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");
            var message = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, message);
            }
            return RedirectToPage();
        }
    }
}
