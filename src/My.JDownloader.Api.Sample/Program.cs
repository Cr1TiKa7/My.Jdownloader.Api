using System;

namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler();
            _jdownloaderHandler.Connect("thetrust3343@yahoo.de", "Cocacola123");
            var devices = _jdownloaderHandler.GetDevices();
            _jdownloaderHandler.AccountsV2.AddAccount(devices[0],"mega.co.nz", "test", "test123");
            //_jdownloaderHandler.LinkgrabberV2.AddLinks(
            //    devices[0],
            //    "https://www.filecrypt.cc/Container/C85E8B070F.html",
            //    "Deadpool 2", "Z:\\Filme\\Deadpool 2");
            _jdownloaderHandler.LinkgrabberV2.QueryLinks(devices[0], 5);
            Console.WriteLine(_jdownloaderHandler.LinkgrabberV2.GetPackageCount(devices[0]));
            Console.ReadLine();
            // _jdownloaderHandler.StartDownload(devices[0]);
        }
    }
}
