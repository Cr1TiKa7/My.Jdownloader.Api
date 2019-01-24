using My.JDownloader.Api.Models.Devices;

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
                //Linq version
                devices.ForEach(x => jdownloaderHandler.GetDeviceHandler(x).AccountsV2.AddAccount("mega.co.nz", "YOURMEAIL", "YOURPASSWORD"));
                //Normal version
                foreach (DeviceObject device in devices)
                {
                    var dHandler = jdownloaderHandler.GetDeviceHandler(device);
                    dHandler.AccountsV2.AddAccount("mega.co.nz", "YOURMEAIL", "YOURPASSWORD");
                }
            }
        }
        
    }
}
