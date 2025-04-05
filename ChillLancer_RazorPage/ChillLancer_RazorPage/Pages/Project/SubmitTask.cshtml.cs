using ChillLancer_RazorPage.Pages.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ChillLancer_RazorPage.Pages.Project
{
    public class SubmitTaskModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SubmitTaskModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Guid ProcessId { get; set; }

        public IActionResult OnGet(Guid processId)
        {
            ProcessId = processId;
            var userRole = _httpContextAccessor.HttpContext?.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userRole)) return Redirect("/index");

            _httpContextAccessor.HttpContext?.Session.SetString("processId", processId.ToString());
            return Page();
        }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        [BindProperty]
        public string Link { get; set; }

        [BindProperty]
        public string Text { get; set; }

        public string WarningMessage { get; set; }

        // Handle the form submission for the image
        public async Task<IActionResult> OnPostUploadImageAsync()
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                WarningMessage = "Please upload a valid image.";
                return Page(); // Stay on the same page and show warning
            }
            string processId = _httpContextAccessor.HttpContext?.Session.GetString("processId");
            using var content = new MultipartFormDataContent();
            using var stream = ImageFile.OpenReadStream();

            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(ImageFile.ContentType);

            content.Add(streamContent, "file", ImageFile.FileName);

            var response = await _httpClient.PostAsync(EndpointConst.baseUrl + "process/"+processId+"/submitTask", content);
            if (response.IsSuccessStatusCode)
            {
                return Redirect("process?processId="+processId);
            }

            return RedirectToPage();
        }

        // Handle the form submission for the link
        public async Task<IActionResult> OnPostUploadLinkAsync()
        {
            if (string.IsNullOrEmpty(Link))
            {
                WarningMessage = "Text must not be empty.";
                return Page(); // Stay on the same page and show warning
            }
            string processId = _httpContextAccessor.HttpContext?.Session.GetString("processId");
            var content = new StringContent("", Encoding.UTF8, "multipart/form-data");
            var response = await _httpClient.PostAsync(EndpointConst.baseUrl + "process/" + processId + "/submitTask?url=" + Link,content);
            if (response.IsSuccessStatusCode)
            {
                return Redirect("process?processId=" + processId);
            }

            return RedirectToPage();
        }

        // Handle the form submission for the text
        public async Task<IActionResult> OnPostUploadTextAsync()
        {
            if (string.IsNullOrEmpty(Text))
            {
                WarningMessage = "Please enter some text.";
                return Page(); // Stay on the same page and show warning
            }
            string processId = _httpContextAccessor.HttpContext?.Session.GetString("processId");
            var content = new StringContent("", Encoding.UTF8, "multipart/form-data");
            var response = await _httpClient.PostAsync(EndpointConst.baseUrl + "process/" + processId + "/submitTask?text=" + Text, content);
            if (response.IsSuccessStatusCode)
            {
                return Redirect("process?processId=" + processId);
            }
            // Process the text (store, validate, etc.)
            return RedirectToPage();
        }
    }
}

public class TaskSubmission
{
    public string? Link { get; set; }
    public string? Text { get; set; }
    public IFormFile? Image { get; set; }
}