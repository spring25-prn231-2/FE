using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class ProposalsModel(HttpClient httpClient) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;

        public ProjectModel Project { get; set; } = new();
        public IList<ProjectModel> Projects { get; set; } = [];
        public IList<ProposalModel> Proposals { get; set; } = [];
        public async Task OnGetAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.proposal}{id}";
            string requestUrl1 = $"{EndpointConst.baseUrl}{EndpointConst.project}{id}";
            var result = await _httpClient.GetAsync(requestUrl1);
            Project = result.IsSuccessStatusCode
                    ? await result.Content.ReadFromJsonAsync<ProjectModel>() ?? new() : new();

            //Project = result.IsSuccessStatusCode
            //        ? await result.Content.ReadFromJsonAsync<ProjectModel>() ?? new() : new();
            // first new() is if api called successed but return null then an empty object will still be returned instead of null which might cause error
            //second new() is if api called failed then an empty object will also be initialized to avoid null object error
            var proposals = await _httpClient.GetAsync(requestUrl);
            Proposals = proposals.IsSuccessStatusCode 
                ? await proposals.Content.ReadFromJsonAsync<List<ProposalModel>>() ?? [] : [];
        }
    }
}
