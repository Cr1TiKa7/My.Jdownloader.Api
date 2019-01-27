using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Config
{
    public class EnumOption
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
