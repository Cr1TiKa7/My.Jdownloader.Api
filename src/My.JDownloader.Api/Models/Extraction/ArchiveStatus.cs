using System.Collections.Generic;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Extraction
{
    public class ArchiveStatus
    {
        [JsonProperty(PropertyName = "archiveId")]
        public string ArchiveId { get; set; }
        [JsonProperty(PropertyName = "archiveName")]
        public string ArchiveName { get; set; }
        [JsonProperty(PropertyName = "controllerId")]
        public long ControllerId { get; set; }
        [JsonProperty(PropertyName = "controllerStatus")]
        public ControllerStatus ControllerStatus { get; set; }
        [JsonProperty(PropertyName = "states")]
        public Dictionary<string,ArchiveFileStatus> States { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
