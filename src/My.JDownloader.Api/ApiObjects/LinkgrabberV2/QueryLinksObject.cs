namespace My.JDownloader.Api.ApiObjects.LinkgrabberV2
{
    public class QueryLinksObject
    {
        [Newtonsoft.Json.JsonProperty(PropertyName ="availability")]
        public bool Availability { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "url")]
        public bool Url { get; set; }
    }
}
