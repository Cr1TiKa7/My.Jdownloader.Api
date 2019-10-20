using System.Collections.Generic;
using My.JDownloader.Api.Models.Extraction.Types;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Extraction.Response
{
    public class ArchiveStatusResponse
    {
        [JsonProperty(PropertyName = "archiveId")]
        public string ArchiveId { get; set; }
        [JsonProperty(PropertyName = "archiveName")]
        public string ArchiveName { get; set; }
        [JsonProperty(PropertyName = "controllerId")]
        public long ControllerId { get; set; }
        [JsonProperty(PropertyName = "controllerStatus")]
        public ControllerStatusTypes ControllerStatus { get; set; }
        [JsonProperty(PropertyName = "states")]
        public Dictionary<string,ArchiveFileStatusType> States { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
