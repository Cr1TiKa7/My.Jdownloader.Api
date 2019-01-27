using System.Collections.Generic;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Dialog
{
    public class DialogInfo
    {
        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string,string> Properties{ get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type{ get; set; }
    }
}
