using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Config
{
    public class AdvancedConfigApiEntry
    {
        [JsonProperty(PropertyName = "abstractType")]
        public AbstractType AbstractType { get; set; }
        [JsonProperty(PropertyName = "defaultValue")]
        public object DefaultValue{ get; set; }
        [JsonProperty(PropertyName = "docs")]
        public string Docs { get; set; }
        [JsonProperty(PropertyName = "enumLabel")]
        public string EnumLabel { get; set; }
        [JsonProperty(PropertyName = "enumOptions")]
        public object EnumOptions { get; set; }
        [JsonProperty(PropertyName = "interfaceName")]
        public string InterfaceName { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "storage")]
        public string Storage { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }
    }
}
