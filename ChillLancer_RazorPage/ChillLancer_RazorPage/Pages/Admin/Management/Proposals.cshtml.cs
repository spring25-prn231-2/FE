using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace ChillLancer_RazorPage.Pages.Admin.Management
{
    public class ProposalsModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public IList<ProposalModel> Proposals { get; set; } = [];
        public async Task OnGetAsync(Guid id)
        {
            await LoadProposalListAsync(id);
        }
        private async Task LoadProposalListAsync(Guid id)
        {
            string requestUrl = $"{EndpointConst.baseUrl}{EndpointConst.proposal}{id}";
            var proposals = await _httpClient.GetAsync(requestUrl);
            Proposals = proposals.IsSuccessStatusCode
                ? await proposals.Content.ReadFromJsonAsync<List<ProposalModel>>() ?? [] : [];
        }
    }
}
