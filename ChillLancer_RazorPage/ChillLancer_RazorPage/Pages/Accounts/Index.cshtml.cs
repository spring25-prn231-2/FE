using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChillLancer_RazorPage.Model.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChillLancer_RazorPage.Models;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        [BindProperty]
        public List<AccountDto> Accounts { get; set; } = new List<AccountDto>();

        // ... other code

        public async Task OnGetAsync()
        {
            try
            {
                var result = await _httpClient.GetAsync(EndpointConst.baseUrl + EndpointConst.account);

                if (result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    Console.WriteLine("Raw JSON Response: " + jsonString);

                    var response = JsonSerializer.Deserialize<ResponseModel>(jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (response is not null && response.value is not null && response.value.data is not null)
                    {
                        var dataJson = JsonSerializer.Serialize(response.value.data);
                        Console.WriteLine("Raw JSON Data: " + dataJson);

                        try
                        {
                            Accounts = JsonSerializer.Deserialize<List<AccountDto>>(dataJson,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error deserializing data: " + ex.Message);
                            Console.WriteLine("Stack Trace: " + ex.StackTrace);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Response or data is null.");
                    }
                }
                else
                {
                    Console.WriteLine($"API call failed with status: {result.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
        }


    }
}
