using System;

namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler();
            _jdownloaderHandler.Connect("email", "password");
            if (_jdownloaderHandler.IsConnected)
            {
                var devices = _jdownloaderHandler.GetDevices();

                if (devices.Count > 0)
                {
                    var dHandler = _jdownloaderHandler.GetDeviceHandler(devices[0]);
                    dHandler.AccountsV2.AddAccount("mega.co.nz", "testWithoutDirectConnection", "test123");
                    if (dHandler.DirectConnect())
                    {
                        dHandler.AccountsV2.AddAccount("mega.co.nz", "testWithDirectConnection", "test123");
                    }
                }
            }
        }
    }
}
