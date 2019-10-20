using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.LinkgrabberV2
{
    public class AddContainer
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}
