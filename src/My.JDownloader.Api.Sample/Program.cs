using System;

namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler();
            _jdownloaderHandler.Connect("test123@test.com", "test");
            if (_jdownloaderHandler.IsConnected)
            {
                var devices = _jdownloaderHandler.GetDevices();

                if (devices.Count > 0)
                {
                    if (_jdownloaderHandler.AccountsV2.AddAccount(devices[0], "mega.co.nz", "test123@test.com",
                        "test123"))
                    {
                        Console.WriteLine("Account successfully added.");
                    }
                }
            }
        }
    }
}
