using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Update
    {
        private readonly JDownloaderApiHandler _ApiHandler;

        internal Update(JDownloaderApiHandler apiHandler)
        {
            _ApiHandler = apiHandler;
        }

        /// <summary>
        /// Checks if the client has an update available.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <returns>True if an update is available.</returns>
        public bool IsUpdateAvailable(DeviceObject device)
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/update/isUpdateAvailable",
                null, JDownloaderHandler.LoginObject, true);
            return response?.Data != null && (bool)response.Data ;
        }

        /// <summary>
        /// Restarts the client and starts the update.
        /// </summary>
        /// <param name="device">The target device.</param>
        public void RestartAndUpdate(DeviceObject device)
        {
            _ApiHandler.CallAction<object>(device, "/update/restartAndUpdate",
                null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Start the update check on the client.
        /// </summary>
        /// <param name="device">The target device.</param>
        public void RunUpdateCheck(DeviceObject device)
        {
            _ApiHandler.CallAction<object>(device, "/update/runUpdateCheck",
                null, JDownloaderHandler.LoginObject, true);
        }
    }
}
