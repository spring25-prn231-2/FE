using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChillLancer_RazorPage.Model.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Accounts
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

        public IList<AccountDto> Accounts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            //_httpContextAccessor.HttpContext?.Session.Remove("Id");

            //int? customerId = _httpContextAccessor.HttpContext?.Session.GetInt32("Id");
            //EmployeeId = _httpContextAccessor.HttpContext?.Session.GetInt32("Id") ?? 0;

            var result = await _httpClient.GetAsync(EndpointConst.baseUrl+EndpointConst.account);

            Accounts = result.IsSuccessStatusCode
                    ? await result.Content.ReadFromJsonAsync<List<AccountDto>>() ?? new List<AccountDto>()
                    : new List<AccountDto>();
        }
    }
}
