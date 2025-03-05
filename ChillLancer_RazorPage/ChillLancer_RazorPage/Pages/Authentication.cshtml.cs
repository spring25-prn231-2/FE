using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Pages
{
    public class AuthenticationModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationModel(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuthRequestModel AuthRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var freelancerResponse = await _httpClient.PostAsJsonAsync("https://localhost:7225/login?Email=1&Password=1", AuthRequest);
            var employeeResponse = await _httpClient.PostAsJsonAsync("https://localhost:7225/login", AuthRequest);

            //if (customerResponse.IsSuccessStatusCode)
            //{
            //    var customer = customerResponse.Content.ReadFromJsonAsync<AuthCustomerResponseModel>().Result;
            //    if (customer is not null)
            //    {
            //        _httpContextAccessor.HttpContext?.Session.SetInt32("CustomerId", customer.Id);
            //        _httpContextAccessor.HttpContext?.Session.SetString("CustomerName", customer.Name);
            //        _httpContextAccessor.HttpContext?.Session.SetString("CustomerToken", customer.AccessToken);

            //        // TODO: change the url
            //        return RedirectToPage("/Appointments/Index");
            //    }
            //}
            //else if (employeeResponse.IsSuccessStatusCode)
            //{
            var employee = employeeResponse.Content.ReadFromJsonAsync<AuthEmployeeResponseModel>().Result;
            if (employee is not null)
            {
                _httpContextAccessor.HttpContext?.Session.SetString("EmpId", employee.Id.ToString());
                _httpContextAccessor.HttpContext?.Session.SetString("EmpName", employee.Name);
                _httpContextAccessor.HttpContext?.Session.SetString("EmpRole", employee.Role);
                //_httpContextAccessor.HttpContext?.Session.SetString("EmpToken", employee.AccessToken);

                return Redirect("/project");
            }
            //}
            // Login failed
            ModelState.AddModelError(string.Empty, "Incorrect email or password!");
            return Page();
        }
    }

    public class AuthRequestModel
    {
        [Required(ErrorMessage = "Field is required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Field is required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }

    public class AuthFreelancerResponseModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string AccessToken { get; set; } = null!;
    }

    public class AuthEmployeeResponseModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("name-tag")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;

        //public string AccessToken { get; set; } = null!;
    }
}
