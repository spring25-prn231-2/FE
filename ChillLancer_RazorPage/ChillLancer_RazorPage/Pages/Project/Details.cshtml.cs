using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class DetailsModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public ProjectModel Project { get; set; } = new();
        [BindProperty]
        public ProposalModel Proposal { get; set; } = new();
        public AccountModel Client { get; set; } = new();
        public async Task OnGetAsync(Guid Id)
        {
            this.Id = Id; // Store Id for reuse
            await LoadProjectAsync(Id);
        }
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            string empIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserProfile") ?? "";

            //Deserialize json format
            if (!string.IsNullOrEmpty(empIdString))
            {
                try
                {
                    using JsonDocument doc = JsonDocument.Parse(empIdString);
                    empIdString = doc.RootElement
                        .GetProperty("value")
                        .GetProperty("data")
                        .GetProperty("id")
                        .GetString() ?? "";
                }
                catch (JsonException)
                {
                    empIdString = ""; // Handle invalid JSON case
                }
            }

            string getAccountAPI = $"{EndpointConst.baseUrl}{EndpointConst.account}{empIdString}";
            string submitProposalAPI = $"{EndpointConst.baseUrl}{EndpointConst.proposal}";
            if (Guid.TryParse(empIdString, out Guid accountId))
            {
                var result = await _httpClient.GetAsync(getAccountAPI);
                Client = result.IsSuccessStatusCode
                ? await result.Content.ReadFromJsonAsync<AccountModel>() ?? new() : new();
                Proposal.FreelancerName = Client.FullName;
                Proposal.AccountId = Client.Id;
                Proposal.ProjectId = id;
                var proposal = await _httpClient.PostAsJsonAsync(submitProposalAPI, Proposal);
                //var response = await result.Content.ReadFromJsonAsync<ResponseModel>();
                if (proposal.IsSuccessStatusCode)
                    return RedirectToPage();
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while submitting the form. ");
                    await LoadProjectAsync(Id);
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You must sign in first!");
                await LoadProjectAsync(Id);
                return Page();
            }
            
        }
        private async Task LoadProjectAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.project}{id}";
            var result = await _httpClient.GetAsync(requestUrl);

            Project = result.IsSuccessStatusCode
                ? await result.Content.ReadFromJsonAsync<ProjectModel>() ?? new()
                : new();
        }
    }
}
