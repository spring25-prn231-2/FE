using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class ProcessModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public ProcessMod process { get; set; }

        [BindProperty]
        public string userRole { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid processId)
        {
            string url = EndpointConst.baseUrl + "process/" + processId;

            userRole = _httpContextAccessor.HttpContext?.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userRole)) return Redirect("/index");

            var result = await _httpClient.GetAsync(url);
            process = result.IsSuccessStatusCode
                ? await result.Content.ReadFromJsonAsync<ProcessMod>() ?? new ProcessMod()
                : new ProcessMod();
            return Page();
        }
    }
}

public class ProcessMod
{
    public Guid Id { get; set; }
    [JsonPropertyName("task-name")]
    public string TaskName { get; set; } = null!;
    [JsonPropertyName("task-description")]
    public string? TaskDescription { get; set; }
    [JsonPropertyName("start-date")]
    public DateTime? StartDate { get; set; }
    [JsonPropertyName("end-date")]
    public DateTime? EndDate { get; set; }
    public decimal Cost { get; set; } = 0;
    [JsonPropertyName("is-paid")]
    public bool IsPaid { get; set; } = false;
    public string? Note { get; set; }
    public string Status { get; set; }
    [JsonPropertyName("proposal-id")]
    public Guid ProposalId { get; set; }

}