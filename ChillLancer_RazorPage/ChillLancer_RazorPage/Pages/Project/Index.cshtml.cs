using ChillLancer_RazorPage.Model.AccountDtos;
using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Models.ViewModels;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using NuGet.Packaging;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [BindProperty]
        public IList<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
        [BindProperty]
        public string EmployerId { get; set; }
        [BindProperty]
        public string EmployerName { get; set; }

        public async Task OnGetAsync()
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("token"));
                var result = await _httpClient.GetAsync(EndpointConst.baseUrl + "project/employer-projects");
                if (result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    Console.WriteLine("Raw JSON Response: " + jsonString);
                    var response = JsonSerializer.Deserialize<ResponseModel>(jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (response is not null && response.value is not null && response.value.data is not null)
                    {
                        var dataJson = JsonSerializer.Serialize(response.value.data);
                        Console.WriteLine("Raw JSON Data: " + dataJson);
                        try
                        {
                            Projects = JsonSerializer.Deserialize<List<ProjectModel>>(dataJson,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            Console.WriteLine("Stack Trace: " + ex.StackTrace);
                        }
                    }
                }
                // get employer info

                var employer = await _httpClient.GetAsync(EndpointConst.baseUrl + "account/profile");
                if (employer.IsSuccessStatusCode)
                {
                    var jsonString = await employer.Content.ReadAsStringAsync();
                    Console.WriteLine("Raw JSON Response: " + jsonString);
                    var response = JsonSerializer.Deserialize<ResponseModelOne>(jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (response is not null && response.value is not null && response.value.data is not null)
                    {
                        var dataJson = JsonSerializer.Serialize(response.value.data);
                        Console.WriteLine("Raw JSON Data: " + dataJson);
                        try
                        {
                            var employerData = JsonSerializer.Deserialize<AccountDto>(dataJson,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            EmployerId = employerData.Id.ToString();
                            EmployerName = employerData.FullName;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            Console.WriteLine("Stack Trace: " + ex.StackTrace);
                        }
                    }
                }
                // get category info
                foreach (var project in Projects)
                {
                    var category = await _httpClient.GetAsync(EndpointConst.baseUrl + "category/" + project.CategoryId.ToString());
                    if (category.IsSuccessStatusCode)
                    {
                        var jsonString = await category.Content.ReadAsStringAsync();
                        Console.WriteLine("Raw JSON Response: " + jsonString);
                        var response = JsonSerializer.Deserialize<CategoryVM>(jsonString,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (response is not null)
                        {
                           project.CategoryName = response.MajorName;
                        }
                    }
                }
                // get skill info
                foreach (var project in Projects)
                {
                    foreach (var skillId in project.SkillIds ?? new List<string>())
                    {
                        var skill = await _httpClient.GetAsync(EndpointConst.baseUrl + "skill/project/" + project.Id);
                        if (skill.IsSuccessStatusCode)
                        {
                            var jsonString = await skill.Content.ReadAsStringAsync();
                            Console.WriteLine("Raw JSON Response: " + jsonString);
                            var response = JsonSerializer.Deserialize<ResponseModel>(jsonString,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            if (response is not null && response.value is not null && response.value.data is not null)
                            {
                                var dataJson = JsonSerializer.Serialize(response.value.data);
                                Console.WriteLine("Raw JSON Data: " + dataJson);
                                try
                                {
                                    var skills = JsonSerializer.Deserialize<List<SkillModel>>(dataJson,
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                    foreach (var skillModel in skills)
                                    {
                                        project.SkillName ??= new List<string>();
                                        project.SkillName.Add(skillModel.Name);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error: " + ex.Message);
                                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }

        }
        public class ProjectSkill
        {
            [JsonPropertyName("project-id")]
            public string ProjectId { get; set; }
            [JsonPropertyName("skill-id")]
            public string SkillId { get; set; }
        }
    }
}
