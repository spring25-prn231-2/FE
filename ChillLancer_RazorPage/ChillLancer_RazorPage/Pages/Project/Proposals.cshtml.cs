using ChillLancer_RazorPage.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class ProposalsModel(HttpClient httpClient) : PageModel
    {
        private readonly HttpClient _httpClient = httpClient;

        public ProjectModel Project { get; set; } = new();
        public IList<ProjectModel> Projects { get; set; } = [];
        public IList<ProposalModel> Proposals { get; set; } = [];
        public async Task OnGetAsync()
        {
            //hard coding, replace the id to suitable for your case, it will fix later when the system has "project details api"
            string requestUrl = $"https://localhost:7225/api/project/a606ffd9-2d8f-4e45-a4d8-810d1a9aec8b/proposals";
            string requestUrl1 = $"https://localhost:7225/api/project";
            var result = await _httpClient.GetAsync(requestUrl1);
            Projects = result.IsSuccessStatusCode
                    ? await result.Content.ReadFromJsonAsync<List<ProjectModel>>() ?? [] : [];

            //hard code, will fix later
            foreach (ProjectModel project in Projects)
            {
                if (project.Id == Guid.Parse("f09a1648-c7ea-4465-9a90-dd87086daa53"))
                {
                    Project = project;
                    break;
                }
            }
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
