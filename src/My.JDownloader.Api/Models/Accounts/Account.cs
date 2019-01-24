using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Accounts
{
    public class Account
    {
        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }
        [JsonProperty(PropertyName = "uuid")]
        public long Uuid { get; set; }
    }
}
