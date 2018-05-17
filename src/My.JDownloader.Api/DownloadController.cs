using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Devices;

namespace My.JDownloader.Api
{
    public class DownloadController
    {
        private readonly JDownloaderApiHandler _ApiHandler;

        internal DownloadController(JDownloaderApiHandler apiHandler)
        {
            _ApiHandler = apiHandler;
        }

        public void StartDownload(DeviceObject device)
        {
            var result = _ApiHandler.CallAction<object>(device, "/downloadcontroller/stop", null, JDownloaderHandler.LoginObject);
        }

        public void StopDownload(DeviceObject device)
        {
            var result = _ApiHandler.CallAction<object>(device, "/downloadcontroller/start", null, JDownloaderHandler.LoginObject);
        }

    }
}
