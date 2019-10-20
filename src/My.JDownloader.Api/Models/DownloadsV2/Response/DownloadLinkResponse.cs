using My.JDownloader.Api.Models.LinkgrabberV2.Types;

namespace My.JDownloader.Api.Models.DownloadsV2.Response
{
    public class DownloadLinkResponse
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "addedDate")]
        public long AddedDate { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "bytesLoaded")]
        public long BytesLoaded { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "bytesTotal")]
        public long BytesTotal { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "downloadPassword")]
        public string  DownloadPassword{ get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "eta")]
        public long Eta { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "extractionStatus")]
        public string ExtractionStatus { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "finished")]
        public bool Finished { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "finishedDate")]
        public long FinishedDate{ get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "host")]
        public string Host { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "packageUUID")]
        public long PackageUUID { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "priority")]
        public PriorityType Priority { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "running")]
        public bool Running { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "skipped")]
        public bool Skipped { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "speed")]
        public long Speed { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "statusIconKey")]
        public string StatusIconKey { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "uuid")]
        public long UUID { get; set; }
    }
}
