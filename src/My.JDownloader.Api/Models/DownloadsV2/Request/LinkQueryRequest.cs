namespace My.JDownloader.Api.Models.DownloadsV2.Request
{
    public class LinkQueryRequest
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "addedDate")]
        public bool AddedDate { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "bytesLoaded")]
        public bool BytesLoaded { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "bytesTotal")]
        public bool BytesTotal { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "comment")]
        public bool Comment { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "eta")]
        public bool Eta { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "extractionStatus")]
        public bool ExtractionStatus { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "finished")]
        public bool Finished { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "finishedDate")]
        public bool FinishedDate { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "host")]
        public bool Host { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "jobUUIDs")]
        public long[] JobUUIDs { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; } = 20;

        [Newtonsoft.Json.JsonProperty(PropertyName = "packageUUIDs")]
        public long[] PackageUUIDs { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "password")]
        public bool Password { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "priority")]
        public bool Priority { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "running")]
        public bool Running { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "skipped")]
        public bool Skipped { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "speed")]
        public bool Speed { get; set; } = true;
        [Newtonsoft.Json.JsonProperty(PropertyName = "saveTo")]
        public bool SaveTo { get; set; } = true;
        [Newtonsoft.Json.JsonProperty(PropertyName = "startAt")]
        public int StartAt { get; set; } 

        [Newtonsoft.Json.JsonProperty(PropertyName = "status")]
        public bool Status { get; set; } = true;

        [Newtonsoft.Json.JsonProperty(PropertyName = "url")]
        public bool Url { get; set; }
    }
}