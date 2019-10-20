using System.Collections.Generic;

namespace My.JDownloader.Api.Models.Devices.Response
{
    public class DeviceJsonResponse
    {
        [Newtonsoft.Json.JsonProperty(PropertyName ="list")]
        public List<Device> Devices { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "rid")]
        public int RequestId { get; set; }
    }
}
