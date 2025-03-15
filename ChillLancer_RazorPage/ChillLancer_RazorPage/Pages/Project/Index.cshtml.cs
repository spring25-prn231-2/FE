using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Models.ResponseModels;
using ChillLancer_RazorPage.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public IndexModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;

            //var token = _httpContextAccessor.HttpContext?.Session.GetString("CustomerToken") ??
            //            _httpContextAccessor.HttpContext?.Session.GetString("EmpToken");

            //if (!string.IsNullOrEmpty(token))
            //{
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
        }

        //=================[ Declare Object Use in Frontend ]=================
        public IList<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
        public List<ProjectVM> ProjectList { get; set; } = new List<ProjectVM>();
        //public int EmployeeId { get; set; }

        //=================[ Methods Handle Frontend Action ]=================
        public async Task OnGetAsync(string categoryName)
        {
            var serverUrl = _configuration["ServerUrl"];
            var apiUrl = $"{serverUrl}/api/project";

            //_httpContextAccessor.HttpContext?.Session.Remove("AppointmentId");

            //int? customerId = _httpContextAccessor.HttpContext?.Session.GetInt32("CustomerId");
            //EmployeeId = _httpContextAccessor.HttpContext?.Session.GetInt32("EmpId") ?? 0;

            string requestUrl = string.Empty;

            requestUrl = $"{apiUrl}/projects";
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
            try
            {
                if (string.IsNullOrEmpty(categoryName))
                {
                    var result = await _httpClient.GetAsync(requestUrl);
                    Projects = result.IsSuccessStatusCode
                        ? await result.Content.ReadFromJsonAsync<List<ProjectModel>>() ?? new List<ProjectModel>()
                        : new List<ProjectModel>();

                }
                else
                {
                    var response = await _httpClient.GetFromJsonAsync<List<ProjectVM>>($"{apiUrl}/category/{categoryName}");
                    if (response != null)
                    {
                        ProjectList = response;
                    }

                    ViewData["CategoryName"] = categoryName;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Fetch project error: " + ex.Message);
            }
        }
    }
}
