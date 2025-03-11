using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChillLancer_RazorPage.Model.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChillLancer_RazorPage.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountCreateDto Account { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _httpClient.PostAsJsonAsync(EndpointConst.baseUrl + EndpointConst.account, Account);
            //if (result.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var stylists = result.Content.ReadFromJsonAsync<List<Entity>>().Result;
            //    availableStylist = stylists;
            //    ViewData["Id"] = new SelectList(list, "Id", "Name");
            //}
            //else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{

            //}

            //var result = await _httpClient.PostAsync(ApplicationEndpoint.Endpoint, JsonContent.Create("string"));
            //if (result.IsSuccessStatusCode)
            //{

            //}
            //else
            //{
            //}

            return RedirectToPage("./Index");
        }
    }
}
