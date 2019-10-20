using System.Collections.Generic;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Dialog.Response
{
    public class DialogTypeInfoResponse
    {
        [JsonProperty(PropertyName = "in")]
        public Dictionary<string, string> In { get; set; }
        [JsonProperty(PropertyName = "out")]
        public Dictionary<string, string> Out { get; set; }
    }
}
