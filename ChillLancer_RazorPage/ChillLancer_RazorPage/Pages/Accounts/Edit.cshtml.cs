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
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
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

        [BindProperty]
        public AccountDto Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = new AccountDto();
            /*await _httpClient.GetAsync(ApplicationEndpoint.GetByIdEndPoint + id)*/
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var api = ApplicationEndpoint.Endpoint + string";
            //try
            //{
            //    var content = new StringContent(JsonConvert.SerializeObject(Service), Encoding.UTF8, "application/json");
            //    var result = await _httpClient.PutAsync(api, content);
            //    if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            //        return RedirectToPage("./Index");
            //}
            //catch (HttpRequestException ex)
            //{
            //    ModelState.AddModelError(string.Empty, $"{ex.Message}");
            //    //return Page();
            //}

            return RedirectToPage("./Index");
        }
    }
}
