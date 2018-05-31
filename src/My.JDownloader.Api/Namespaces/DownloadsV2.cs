using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.DownloadsV2;

namespace My.JDownloader.Api.Namespaces
{
    public class DownloadsV2
    {
        private readonly JDownloaderApiHandler _ApiHandler;
        private readonly DeviceObject _Device;

        internal DownloadsV2(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            _ApiHandler = apiHandler;
            _Device = device;
        }

        /// <summary>
        /// Gets the stop mark as long.
        /// </summary>
        /// <returns>The stop mark as long.</returns>
        public long GetStopMark()
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/linkgrabberv2/getStopMark", null, JDownloaderHandler.LoginObject);
            if (response?.Data == null)
                return -1;

            return (long)response.Data;
        }

        /// <summary>
        /// Gets informations about a stop marked link.
        /// </summary>
        /// <returns>Returns informations about a stop marked link.</returns>
        public StopMarkedLinkReturnObject GetStopMarkedLink()
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/linkgrabberv2/getStopMark", null, JDownloaderHandler.LoginObject);

            return (StopMarkedLinkReturnObject)response?.Data;
        }

    }
}
