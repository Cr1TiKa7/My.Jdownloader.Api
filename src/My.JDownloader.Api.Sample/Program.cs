namespace My.JDownloader.Api.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            JDownloaderHandler _jdownloaderHandler = new JDownloaderHandler();
            _jdownloaderHandler.Connect("EMAIL", "PASSWORD");
            var devices = _jdownloaderHandler.GetDevices();
            _jdownloaderHandler.AddLink(
                devices[0],
                "http://www.google.de",
                "TEST PACKAGE NAME","C:/");
            _jdownloaderHandler.StartDownload(devices[0]);
        }
    }
}
