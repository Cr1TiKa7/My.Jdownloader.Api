﻿using My.JDownloader.Api.Models.LinkgrabberV2;

namespace My.JDownloader.Api.Models.DownloadsV2
{
    public class FilePackageObject
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "activeTask")]
        public string ActiveTask { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "bytesLoaded")]
        public long BytesLoaded { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "bytesTotal")]
        public long BytesTotal { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "childCount")]
        public int ChildCount { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "downloadPassword")]
        public string DownloadPassword { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "eta")]
        public long Eta { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "finished")]
        public bool Finished { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "host")]
        public string Host { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "priority")]
        public PriorityType Priority { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "running")]
        public bool Running { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "saveTo")]
        public string SaveTo { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "speed")]
        public long Speed { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "statusIconKey")]
        public string StatusIconKey { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "uuid")]
        public long UUID { get; set; }
    }
}