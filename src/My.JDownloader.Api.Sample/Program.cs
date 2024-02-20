using My.JDownloader.Api;
using System.Reflection;

JDownloaderHandler jdownloaderHandler = new JDownloaderHandler("USERNAME", "PASSWORD", "APPNAME");
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
		var tmp = dHandler.LinkgrabberV2.QueryPackages(new My.JDownloader.Api.Models.LinkgrabberV2.Request.QueryPackagesRequest());

		foreach (var link in tmp)
		{
			dHandler.DownloadsV2.SetComment(null, new[] { link.UUID }, true, "This is a test");
		}
	}
}