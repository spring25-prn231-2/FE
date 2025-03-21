using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ChillLancer_RazorPage.Models;
using ChillLancer_RazorPage.Models.ViewModels;
using ChillLancer_RazorPage.Pages;
using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Razor.Pages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [BindProperty]
        public ProjectCreateModel Project { get; set; } = default!;
        [BindProperty]
        public List<CategoryVM> Categories { get; set; } = default!;
        [BindProperty]
        public List<SkillModel> Skills { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> CategoryOptions { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> SkillOptions { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            var categories = await _httpClient.GetAsync(EndpointConst.baseUrl + "category");
            var skills = await _httpClient.GetAsync(EndpointConst.baseUrl + "skill");
            if (categories.IsSuccessStatusCode && skills.IsSuccessStatusCode)
            {
                var categoriesJson = await categories.Content.ReadAsStringAsync();
                var skillsJson = await skills.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<CategoryVM>>(categoriesJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            if (skills.IsSuccessStatusCode)
            {
                var skillsJson = await skills.Content.ReadAsStringAsync();
                Skills = JsonSerializer.Deserialize<List<SkillModel>>(skillsJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            CategoryOptions = Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),  // Giá trị gửi về là ID
                Text = c.MajorName              // Hiển thị Name trong dropdown
            }).ToList();
            SkillOptions = Skills.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("token"));
            var response = await _httpClient.PostAsJsonAsync(EndpointConst.baseUrl + "project", Project);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
