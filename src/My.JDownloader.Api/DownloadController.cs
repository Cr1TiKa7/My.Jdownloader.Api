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

    }
}
