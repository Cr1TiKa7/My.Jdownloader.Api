
using My.JDownloader.Api.ApiObjects.Devices;

namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler jdownloaderHandler = new JDownloaderHandler("YOUREMAIL", "YOURPASSWORD","YOURAPPKEY");
            if (jdownloaderHandler.IsConnected)
            {
                var devices = jdownloaderHandler.GetDevices();
                foreach (DeviceObject device in devices)
                {
                    var dHandler = jdownloaderHandler.GetDeviceHandler(device);
                    dHandler.AccountsV2.AddAccount("mega.co.nz", "YOURMEAIL", "YOURPASSWORD");
                }
            }
        }
        
    }
}
