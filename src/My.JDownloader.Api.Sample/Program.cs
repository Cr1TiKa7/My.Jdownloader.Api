using System;

namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler();
            _jdownloaderHandler.Connect("thetrust3343@yahoo.de", "1Q2w3e4r5t!!");
            if (_jdownloaderHandler.IsConnected)
            {
                var devices = _jdownloaderHandler.GetDevices();

                if (devices.Count > 0)
                {
                    var dHandler = _jdownloaderHandler.GetDeviceHandler(devices[0]);
                    dHandler.AccountsV2.AddAccount("mega.co.nz", "teTETSst123", "test123");
                    //if (_jdownloaderHandler.AccountsV2.AddAccount(devices[0], "mega.co.nz", "test123@test.com",
                    //"test123"))
                    //{
                    //Console.WriteLine("Account successfully added.");
                    //}
                }
            }
        }
    }
}
