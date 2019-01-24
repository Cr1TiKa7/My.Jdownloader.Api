using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Accounts
{
    public class ApiQuery
    {
        [JsonProperty(PropertyName = "empty")]
        public bool Empty { get; set; }
        [JsonProperty(PropertyName = "forNullKey")]
        public object ForNullKey{ get; set; }
        [JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; }
        [JsonProperty(PropertyName = "startAt")]
        public int StartAt { get; set; }
    }
}
