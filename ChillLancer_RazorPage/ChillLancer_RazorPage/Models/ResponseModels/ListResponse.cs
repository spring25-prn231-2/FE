using System.Text.Json.Serialization;

namespace ChillLancer_RazorPage.Models.ResponseModels
{
    public class ListResponse<T> where T : class
    {
        [JsonPropertyName("page-index")]
        public int PageIndex { get; set; }

        [JsonPropertyName("page-size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total-count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("total-page")]
        public int TotalPage { get; set; }

        [JsonPropertyName("data-list")]
        public List<T> DataList { get; set; }
    }
}
