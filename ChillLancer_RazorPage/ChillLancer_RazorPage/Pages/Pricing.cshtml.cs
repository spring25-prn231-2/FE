using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages
{
    public class PricingModel(HttpClient httpClient, IConfiguration configuration) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;
        public List<PackageModel> Packages { get; set; } = [];
        public async Task<IActionResult> OnGetAsync()
        {
            var apiUrl = $"{_configuration["ServerUrl"]}/api/package";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PackageModel>>(apiUrl);
                if (response != null)
                {
                    Packages = response;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Fetch packages error: " + ex.Message);
            }
            return Page();
        }

    }
}
