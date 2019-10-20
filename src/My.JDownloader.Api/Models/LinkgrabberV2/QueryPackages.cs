using System.Collections.Generic;
using My.JDownloader.Api.Models.LinkgrabberV2.Response;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.LinkgrabberV2
{
    internal class QueryPackages
    {
        public List<QueryPackagesResponse> Data { get; set; }
        [JsonProperty(PropertyName = "rid")]
        public int RequestId { get; set; }
    }
}
