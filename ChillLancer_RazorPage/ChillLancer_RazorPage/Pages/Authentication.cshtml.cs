using ChillLancer_RazorPage.Model.AccountDtos;
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
        [BindProperty]
        public SignUpRequestModel SignUpRequest { get; set; } = default!;

        // Handle Login Form
        public async Task<IActionResult> OnPostLogin()
        {
            //if (!TryValidateModel(AuthRequest))
            //{
            //    return Page(); // Return the page if validation fails
            //}

            var employeeResponse = await _httpClient.PostAsJsonAsync("https://localhost:7225/login", AuthRequest);

            if (employeeResponse.IsSuccessStatusCode)
            {
                var employee = await employeeResponse.Content.ReadFromJsonAsync<ResponseModel>();
                if (employee is not null)
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("token", employee.value.token);
                    return Redirect("/project"); // Redirect on successful login
                }
            }

            ModelState.AddModelError(string.Empty, "Incorrect email or password!");
            return Page();
        }

        // Handle Sign Up Form
        public async Task<IActionResult> OnPostSignUp()
        {
            //if (!TryValidateModel(SignUpRequest))
            //{
            //    return Page(); // Return the page if validation fails
            //}

            var signUpResponse = await _httpClient.PostAsJsonAsync("https://localhost:7225/register", SignUpRequest);
            var response = await signUpResponse.Content.ReadFromJsonAsync<SignUpResponseModel>();

            if (response.value.code == 200)
            {
                return Page(); // Redirect to login page if registration is successful
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.value.message);
                return Page(); // Return the page with error message
            }
        }
    }

    public class AuthRequestModel
    {
        //[Required(ErrorMessage = "Field is required!", AllowEmptyStrings = false)]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        //[Required(ErrorMessage = "Field is required!", AllowEmptyStrings = false)]
        //[DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }

    public class AuthFreelancerResponseModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string AccessToken { get; set; } = null!;
    }

    public class ResponseModel
    {
        public Value value { get; set; }
    }
    public class ResponseModelOne
    {
        public ValueOne value { get; set; }
    }
    public class ValueOne
    {
        public string token { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        [JsonPropertyName("status-code")]
        public int code { get; set; }
    }

    public class Value
    {
        public string token { get; set; }
        public string message { get; set; }
        public List<object> data { get; set; }
        [JsonPropertyName("status-code")]
        public int code { get; set; }
    }
    public class SignUpRequestModel
    {
        [JsonPropertyName("full-name")]
        public string FullName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
    public class SignUpResponseModel
    {
        public Value value { get; set; }
    }

}
