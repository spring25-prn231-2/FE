using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Admin.System
{
    public class LanguagesModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LanguagesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public List<LanguageVM> LanguagesList { get; set; } = [];

        [BindProperty]
        public LanguageVM LanguageObj { get; set; } = new LanguageVM();

        [BindProperty]
        public bool IsEdit { get; set; } = false;

        //==========================================================================================
        public async Task OnGetAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/language";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<LanguageVM>>(apiUrl);
                if (response != null)
                {
                    LanguagesList = response;
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
            var apiUrl = $"{serverUrl}/api/language";

            if (!IsEdit)
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, LanguageObj);
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
                    Content = JsonContent.Create(LanguageObj)
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
            var apiUrl = $"{serverUrl}/api/language";

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