using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
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

        /// <summary>
        /// Forces JDownloader to start downloading the given links/packages
        /// </summary>
        /// <param name="device">The target device</param>
        /// <param name="linkIds">The ids of the links you want to force download.</param>
        /// <param name="packageIds">The ids of the packages you want to force download.</param>
        /// <returns>True if successfull</returns>
        public bool ForceDownload(DeviceObject device, long[] linkIds, long[] packageIds)
        {
            var param = new[] {linkIds, packageIds};
            var result = _ApiHandler.CallAction<DefaultReturnObject>(device, "/downloadcontroller/forceDownload", param, JDownloaderHandler.LoginObject, true);
            return result != null;
        }

        /// <summary>
        /// Gets the current state of the device
        /// </summary>
        /// <param name="device">The Target device.</param>
        /// <returns>The current state of the device.</returns>
        public string GetCurrentState(DeviceObject device)
        {
            var result = _ApiHandler.CallAction<DefaultReturnObject>(device, "/downloadcontroller/getCurrentState", null, JDownloaderHandler.LoginObject, true);
            if (result != null)
                return (string)result.Data;
            return "UNKOWN_STATE";
        }

        /// <summary>
        /// Gets the actual download speed of the client.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>The actual download speed.</returns>
        public long GetSpeedInBps(DeviceObject device)
        {
            var result = _ApiHandler.CallAction<DefaultReturnObject>(device, "/downloadcontroller/getSpeedInBps", null, JDownloaderHandler.LoginObject, true);
            if (result != null)
                return (long)result.Data;
            return 0;
        }

        /// <summary>
        /// Starts all downloads.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <returns>True if successfull.</returns>
        public bool Start(DeviceObject device)
        {
            var result = _ApiHandler.CallAction<DefaultReturnObject>(device, "/downloadcontroller/stop", null, JDownloaderHandler.LoginObject, true);
            if (result != null)
                return (bool) result.Data;
            return false;
        }

        /// <summary>
        /// Stops all downloads.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <returns>True if successfull.</returns>
        public bool Stop(DeviceObject device)
        {
            var result = _ApiHandler.CallAction<DefaultReturnObject>(device, "/downloadcontroller/start", null, JDownloaderHandler.LoginObject, true);
            if (result != null)
                return (bool)result.Data;
            return false;
        }

        /// <summary>
        /// Pauses all downloads.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="pause">True if you want to pause the download</param>
        /// <returns>True if successfull.</returns>
        public bool Pause(DeviceObject device,bool pause)
        {
            var param = new[] {pause};
            var result = _ApiHandler.CallAction<DefaultReturnObject>(device, "/downloadcontroller/pause", param, JDownloaderHandler.LoginObject, true);
            if (result != null)
                return (bool)result.Data;
            return false;
        }


    }
}
