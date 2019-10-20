using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Config.Request
{
    public class AdvancedConfigQueryRequest
    {
        [JsonProperty(PropertyName = "configInterface")]
        public string ConfigInterface { get; set; }
        [JsonProperty(PropertyName = "defaultValues")]
        public bool DefaultValues { get; set; }
        [JsonProperty(PropertyName = "description")]
        public bool Description { get; set; }
        [JsonProperty(PropertyName = "enumInfo")]
        public bool EnumInfo { get; set; }
        [JsonProperty(PropertyName = "includeExtensions")]
        public bool IncludeExtensions { get; set; }
        [JsonProperty(PropertyName = "Pattern")]
        public string Pattern { get; set; }
        [JsonProperty(PropertyName = "values")]
        public bool Values { get; set; }
    }
}
