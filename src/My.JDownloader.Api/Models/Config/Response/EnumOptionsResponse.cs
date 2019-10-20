using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Config.Response
{
    public class EnumOptionsResponse
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
