using System.Collections.Generic;

namespace My.JDownloader.Api.Models.Devices.Response
{
    public class DeviceConnectionInfoResponse
    {
        public List<DeviceConnectionInfo> Infos { get; set; }
        public bool RebindProtectionDetected { get; set; }
        public string Mode { get; set; }
    }
}
