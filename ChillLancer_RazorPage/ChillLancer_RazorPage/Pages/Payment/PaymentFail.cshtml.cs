using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChillLancer_RazorPage.Pages.Payment
{
    public class PaymentFailModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentFailModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    public void OnGet()
    {
    }
}
}