using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Admin.Management
{
    public class AccountsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AccountsModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public List<AccountVM> AccountsList { get; set; } = [];

        [BindProperty]
        public AccountVM AccountObj { get; set; } = new AccountVM();

        [BindProperty]
        public bool IsEdit { get; set; } = false;

        //==========================================================================================
        public async Task OnGetAsync()
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/account/odata";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<AccountVM>>(apiUrl);
                if (response != null)
                {
                    AccountsList = response;
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
            var apiUrl = $"{serverUrl}/api/account";

            if (!IsEdit)
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, AccountObj);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Add New Failed!");
                    await OnGetAsync();
                    return Page();
                }
            }
            else
            {
                var request = new HttpRequestMessage(new HttpMethod("PUT"), apiUrl)
                {
                    Content = JsonContent.Create(AccountObj)
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
            var apiUrl = $"{serverUrl}/api/account";

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
