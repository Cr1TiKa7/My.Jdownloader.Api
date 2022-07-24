namespace My.JDownloader.Api.Models.DownloadsV2.Request
{
    public class CleanupRequest
    {

        [Newtonsoft.Json.JsonProperty(PropertyName = "linkIds")]
        public long[] LinkIds { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "packageIds")]
        public long[] PackageIds { get; set; }
    }
}
