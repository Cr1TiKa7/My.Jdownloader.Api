using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Extraction
    {
        private readonly JDownloaderApiHandler _ApiHandler;
        private readonly DeviceObject _Device;

        internal Extraction(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            _ApiHandler = apiHandler;
            _Device = device;
        }

        /// <summary>
        /// Adds an archive password to the client.
        /// </summary>
        /// <param name="password">The password to add.</param>
        /// <returns>True if successfull.</returns>
        public bool AddArchivePassword(string password)
        {
            var param = new[] {password};
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/extraction/addArchivePassword",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null;
        }

        /// <summary>
        /// Cancels an extraction process.
        /// </summary>
        /// <param name="controllerId">The id of the controll you want to cancel.</param>
        /// <returns>True if successfull.</returns>
        public bool CancelExtraction(string controllerId)
        {
            var param = new[] { controllerId };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/extraction/cancelExtraction",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }
    }
}
