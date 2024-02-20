using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.LinkgrabberV2.Request;
using My.JDownloader.Api.Models.Types;

namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler jdownloaderHandler = new JDownloaderHandler("sfghtr3dfgga@byom.de", "8Seichen", "APPNAME");
            //jdownloaderHandler.Connect("YOURMEAIL", "YOURPASSWORD");
            if (jdownloaderHandler.IsConnected)
            {
                var devices = jdownloaderHandler.GetDevices();
                //Linq version
                //devices.ForEach(x => jdownloaderHandler.GetDeviceHandler(x).AccountsV2.AddAccount("mega.co.nz", "YOURMEAIL", "YOURPASSWORD"));
                //Normal version

                //jdownloaderHandler.Reconnect();
                foreach (My.JDownloader.Api.Models.Devices.Device device in devices)
                {
					var dHandler = jdownloaderHandler.GetDeviceHandler(device);

					AddLinkRequest addLinkRequest = new AddLinkRequest
					{
						AutoStart = false, // Do not start the download automatically
						AutoExtract = true, // Automatically extract archives
						Priority = "DEFAULT", // Set priority to default
						Links = "https://bunkr.sk/a/oSrJmld2,https://bunkr.sk/a/qLA1tr2q,https://www.youtube.com/watch?v=J3kMiFo0ru4&t=114s", // Demo URLs separated by comma
						PackageName = "Demo Package", // Name of the download package
						DestinationFolder = "/downloads/demo", // Destination folder for downloads
						DownloadPassword = null, // No download password
						ExtractPassword = null, // No extract password
												// OverridePackagizer is always true based on your class definition
					};

                    var sdf = dHandler.LinkgrabberV2.AddLinks(addLinkRequest);
                    var sdfd = dHandler.LinkCrawler.IsCrawling();



                    var tmp = dHandler.LinkgrabberV2.QueryPackages(new Models.LinkgrabberV2.Request.QueryPackagesRequest());

                    foreach (var link in tmp)
                    {
                        dHandler.DownloadsV2.SetComment(null, new[] { link.UUID }, true, "This is a test"); 
                    }
                }
            }
        }
        
    }
}
