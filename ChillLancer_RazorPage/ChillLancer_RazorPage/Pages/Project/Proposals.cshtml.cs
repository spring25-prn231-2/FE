using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class ProposalsModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public ProjectModel Project { get; set; } = new();
        public IList<ProjectModel> Projects { get; set; } = [];
        public IList<ProposalModel> Proposals { get; set; } = [];
        public IList<MilestoneModel> Milestones { get; set; } = [];
        public AccountModel Employer { get; set; } = new();
        public Guid CurrentUserId { get; set; }
        public bool IsUserLoggedIn { get; set; } = false;
        public bool HasAcceptedProposal { get; set; } = false;

        public async Task OnGetAsync(Guid id)
        {
            CheckLogin();
            await LoadProposalListAsync(id);
            await LoadProjectInfoAsync(id);
            await LoadEmployerInfoAsync(id);
            await CheckAcceptedProposalAsync(id);
        }
        private async Task LoadProposalListAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.proposal}{id}";
            var proposals = await _httpClient.GetAsync(requestUrl);
            Proposals = proposals.IsSuccessStatusCode
                ? await proposals.Content.ReadFromJsonAsync<List<ProposalModel>>() ?? [] : [];
        }
        private async Task LoadProjectInfoAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.project}{id}";
            var result = await _httpClient.GetAsync(requestUrl);
            Project = result.IsSuccessStatusCode
                    ? await result.Content.ReadFromJsonAsync<ProjectModel>() ?? new() : new();
            //Project = result.IsSuccessStatusCode
            //        ? await result.Content.ReadFromJsonAsync<ProjectModel>() ?? new() : new();
            // first new() is if api called successed but return null then an empty object will still be returned instead of null which might cause error
            //second new() is if api called failed then an empty object will also be initialized to avoid null object error
        }
        private async Task LoadEmployerInfoAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.account}project/{id}";
            var result = await _httpClient.GetAsync(requestUrl);

            Employer = result.IsSuccessStatusCode
                ? await result.Content.ReadFromJsonAsync<AccountModel>() ?? new()
                : new();
        }
        private async Task CheckAcceptedProposalAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.proposal}checkAcceptedProposal/{id}";
            var result = await _httpClient.GetAsync(requestUrl);
            HasAcceptedProposal = result.IsSuccessStatusCode;
        }
        private void CheckLogin()
        {
            string accountInfo = _httpContextAccessor.HttpContext?.Session.GetString("UserProfile") ?? "";
            if (!string.IsNullOrEmpty(accountInfo))
            {
                IsUserLoggedIn = true;
                try
                {
                    using JsonDocument doc = JsonDocument.Parse(accountInfo);
                    accountInfo = doc.RootElement
                        .GetProperty("value")
                        .GetProperty("data")
                        .GetProperty("id")
                        .GetString() ?? "";
                    CurrentUserId = Guid.Parse(accountInfo);
                }
                catch (JsonException)
                {
                    accountInfo = ""; // Handle invalid JSON case
                    CurrentUserId = Guid.Parse(accountInfo);
                }
            }
            else IsUserLoggedIn = false;
        }
    }
}
