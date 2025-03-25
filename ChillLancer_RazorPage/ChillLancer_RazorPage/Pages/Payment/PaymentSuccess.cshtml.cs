using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Payment
{
    public class PaymentSuccessModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentSuccessModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public int AppointmentId { get; set; }
        public IActionResult OnGet()
        {
            AppointmentId = _httpContextAccessor.HttpContext.Session.GetInt32("AppointmentId") ?? 0;
            return Page();
        }
    }
}