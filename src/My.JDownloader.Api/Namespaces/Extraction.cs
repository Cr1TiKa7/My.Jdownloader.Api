using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Extraction
    {
        private readonly JDownloaderApiHandler _ApiHandler;

        internal Extraction(JDownloaderApiHandler apiHandler)
        {
            _ApiHandler = apiHandler;
        }

        /// <summary>
        /// Adds an archive password to the client.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="password">The password to add.</param>
        /// <returns>True if successfull.</returns>
        public bool AddArchivePassword(DeviceObject device, string password)
        {
            var param = new[] {password};
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extraction/addArchivePassword",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null;
        }

        /// <summary>
        /// Cancels an extraction process.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="controllerId">The id of the controll you want to cancel.</param>
        /// <returns>True if successfull.</returns>
        public bool CancelExtraction(DeviceObject device, string controllerId)
        {
            var param = new[] { controllerId };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extraction/cancelExtraction",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }
    }
}
