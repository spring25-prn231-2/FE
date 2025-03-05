using ChillLancer_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ChillLancer_RazorPage.Pages.Project
{
        public class IndexModel : PageModel
        {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public IndexModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
            {
                _httpClient = httpClient;
                _httpContextAccessor = httpContextAccessor;

                //var token = _httpContextAccessor.HttpContext?.Session.GetString("CustomerToken") ??
                //            _httpContextAccessor.HttpContext?.Session.GetString("EmpToken");

                //if (!string.IsNullOrEmpty(token))
                //{
                //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //}
            }

            public IList<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
            //public int EmployeeId { get; set; }

            public async Task OnGetAsync()
            {
                //_httpContextAccessor.HttpContext?.Session.Remove("AppointmentId");

                //int? customerId = _httpContextAccessor.HttpContext?.Session.GetInt32("CustomerId");
                //EmployeeId = _httpContextAccessor.HttpContext?.Session.GetInt32("EmpId") ?? 0;

                string requestUrl = string.Empty;

                requestUrl = $"https://localhost:7225/api/project/projects";
                //switch (customerId, EmployeeId)
                //{
                //    case (not null, _):
                //        requestUrl = $"";
                //        break;
                //    case (null, > 0):
                //        requestUrl = ;
                //        break;
                //    default:
                //        return;
                //}

                var result = await _httpClient.GetAsync(requestUrl);
                Projects = result.IsSuccessStatusCode
                    ? await result.Content.ReadFromJsonAsync<List<ProjectModel>>() ?? new List<ProjectModel>()
                    : new List<ProjectModel>();
            }
        }
    }
