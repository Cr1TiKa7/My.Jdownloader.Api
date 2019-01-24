using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.DownloadsV2;

namespace My.JDownloader.Api.Namespaces
{
    public class DownloadsV2 : Base
    {

        internal DownloadsV2(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Gets the stop mark as long.
        /// </summary>
        /// <returns>The stop mark as long.</returns>
        public long GetStopMark()
        {
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/getStopMark", null, JDownloaderHandler.LoginObject);
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
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/getStopMark", null, JDownloaderHandler.LoginObject);

            return (StopMarkedLinkReturnObject)response?.Data;
        }

    }
}
