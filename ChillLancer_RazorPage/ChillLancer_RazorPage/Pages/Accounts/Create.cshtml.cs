using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChillLancer_RazorPage.Model.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChillLancer_RazorPage.Models;

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
        public string ErrorMessage { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _httpClient.PostAsJsonAsync(EndpointConst.baseUrl + EndpointConst.account, Account);
            var response = await result.Content.ReadFromJsonAsync<ResponseModel> ();
            if (result.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ErrorMessage = response.value.message;
                return Page();
            }
        }

    }
}
