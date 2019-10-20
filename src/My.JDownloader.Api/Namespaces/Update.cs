using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Update : Base
    {

        internal Update(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Checks if the client has an update available.
        /// </summary>
        /// <returns>True if an update is available.</returns>
        public bool IsUpdateAvailable()
        {
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/update/isUpdateAvailable",
                null, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data ;
        }

        /// <summary>
        /// Restarts the client and starts the update.
        /// </summary>
        public void RestartAndUpdate()
        {
            ApiHandler.CallAction<object>(Device, "/update/restartAndUpdate",
                null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Start the update check on the client.
        /// </summary>
        public void RunUpdateCheck()
        {
            ApiHandler.CallAction<object>(Device, "/update/runUpdateCheck",
                null, JDownloaderHandler.LoginObject, true);
        }
    }
}
