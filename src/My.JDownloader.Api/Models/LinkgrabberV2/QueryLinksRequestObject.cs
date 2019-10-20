namespace My.JDownloader.Api.Models.LinkgrabberV2
{

    public class QueryLinksRequestObject
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "availability")]
        public bool Availability { get; set; }
        [Newtonsoft.Json.JsonProperty("bytesTotal")]
        public bool BytesTotal { get; set; }
        [Newtonsoft.Json.JsonProperty("comment")]
        public bool Comment { get; set; }
        [Newtonsoft.Json.JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [Newtonsoft.Json.JsonProperty("jobUUIDs")]
        public long[] JobUuids{ get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; } = -1;
        [Newtonsoft.Json.JsonProperty("packageUUIDs")]
        public long[] PackageUuids { get; set; }
        [Newtonsoft.Json.JsonProperty("password")]
        public bool Password { get; set; }
        [Newtonsoft.Json.JsonProperty("priority")]
        public bool Priority { get; set; }
        [Newtonsoft.Json.JsonProperty("startAt")]
        public int StartAt { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "url")]
        public bool Url { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "variantID")]
        public bool VariantId { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "variantIcon")]
        public bool VariantIcon { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "variantName")]
        public bool VariantName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "variants")]
        public bool Variants { get; set; }
    }
}
