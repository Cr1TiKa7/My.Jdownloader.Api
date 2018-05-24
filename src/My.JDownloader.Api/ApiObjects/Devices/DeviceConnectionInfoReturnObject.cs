using System.Collections.Generic;

namespace My.JDownloader.Api.ApiObjects.Devices
{
    public class DeviceConnectionInfoReturnObject
    {
        public List<DeviceConnectionInfoObject> Infos { get; set; }
        public bool RebindProtectionDetected { get; set; }
        public string Mode { get; set; }
    }
}
