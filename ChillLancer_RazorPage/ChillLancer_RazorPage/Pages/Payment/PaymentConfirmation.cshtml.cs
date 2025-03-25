using ChillLancer_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChillLancer_RazorPage.Pages.Payment
{
    public class PaymentConfirmationModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentConfirmationModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            var token = _httpContextAccessor.HttpContext?.Session.GetString("CustomerToken") ??
            _httpContextAccessor.HttpContext?.Session.GetString("EmpToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public MilestoneModel process { get; set; }
        public string processId { get; set; }
        public string projectId { get; set; }   
        public string userId { get; set; }


        public async Task<IActionResult> OnGetAsync(string processId)
        {
            await populate(processId);
            if (userId == "" || processId == "")
            {
                ModelState.AddModelError(string.Empty, "Appointment Not found.");
                return Page();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string processId)
        {
            await populate(processId);

            if (userId is null || process is null)
            {
                ModelState.AddModelError(string.Empty, "not found.");
                return Page();
            }

            var url = $"https://localhost:7225/api/transaction/payment/vnpay?orderId={process.Id}&userId={userId}";
            var response = await _httpClient.PostAsync(url, null);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<PaymentResponseModel>();
                if (data != null && data.url != null)
                {
                    return Redirect(data.url);
                }
            }
            ModelState.AddModelError(string.Empty, "Payment unsuccessfully.");
            return Page();
        }

        public async Task populate(string processId)
        {
            string userprofile = _httpContextAccessor.HttpContext.Session.GetString("UserProfile") ?? "";

            using JsonDocument doc = JsonDocument.Parse(userprofile);
            userId = doc
                .RootElement.GetProperty("value")
                .GetProperty("data")
                .GetProperty("id")
                .GetString();

            projectId = _httpContextAccessor.HttpContext.Session.GetString("projectId") ?? "";

            if (userId is not null || processId is not null)
            {
                var result = await _httpClient.GetAsync("https://localhost:7225/api/process/"+ processId);
                    process = result.Content.ReadFromJsonAsync<MilestoneModel>().Result;
            }
        }

        public class PaymentResponseModel
        {
            public string url { get; set; }
        }
    }
}