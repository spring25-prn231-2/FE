using ChillLancer_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class DetailsModel(HttpClient httpClient) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;
        public ProjectModel Project { get; set; } = new();
        public async Task OnGetAsync(Guid Id)
        {
            string requestUrl = $"https://localhost:7225/api/project/{Id}";
            var result = await _httpClient.GetAsync(requestUrl);
            Project = result.IsSuccessStatusCode
                ? await result.Content.ReadFromJsonAsync<ProjectModel>() ?? new() : new();
        }
    }
}
