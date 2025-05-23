using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Admin.System
{
    public class CodesModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CodesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public List<CodeVM> CodesList { get; set; } = [];

        [BindProperty]
        public CodeVM CodeObj { get; set; } = new CodeVM();

        [BindProperty]
        public bool IsEdit { get; set; } = false;

        //==========================================================================================
        public async Task OnGetAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/Code/list?PageSize=999&status=exceptdeleted";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<ListResponse<CodeVM>>(apiUrl);
                if (response != null && response.DataList != null)
                {
                    CodesList = response.DataList;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Fetch Codes error: " + ex.Message);
            }
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/Code";

            if (!IsEdit)
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, CodeObj);
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
                    Content = JsonContent.Create(CodeObj)
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
            var apiUrl = $"{serverUrl}/api/Code";

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
