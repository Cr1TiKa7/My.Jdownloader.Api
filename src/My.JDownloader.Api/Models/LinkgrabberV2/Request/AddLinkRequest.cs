using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.LinkgrabberV2.Request
{
    public class AddLinkRequest
    {
        [JsonProperty(PropertyName ="autostart")]
        public bool AutoStart { get; set; }
        [JsonProperty(PropertyName = "autoextract")]
        public bool AutoExtract { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public string Priority { get; set; }
        [JsonProperty(PropertyName = "links")]
        public string Links { get; set; }
        [JsonProperty(PropertyName = "packageName")]
        public string PackageName { get; set; }
        [JsonProperty(PropertyName = "destinationFolder")]
        public string DestinationFolder { get; set; }
        [JsonProperty(PropertyName = "downloadPassword")]
        public string DownloadPassword { get; set; }
        [JsonProperty(PropertyName = "extractPassword")]
        public string ExtractPassword { get; set; }

        // Thanks to devocalypse
        [JsonProperty(PropertyName = "overwritePackagizerRules")] 
        public bool OverridePackagizer { get; } = true;
    }
}
