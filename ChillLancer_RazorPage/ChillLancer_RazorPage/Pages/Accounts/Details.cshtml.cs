using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChillLancer_RazorPage.Model.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChillLancer_RazorPage.Models;

namespace ChillLancer_RazorPage.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
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

        public AccountModel Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = new AccountModel();
            /*await _httpClient.GetAsync(ApplicationEndpoint.GetByIdEndPoint + id)*/
            if (account == null)
            {
                return NotFound();
            }
            else
            {
                Account = account;
            }
            return Page();
        }
    }
}
