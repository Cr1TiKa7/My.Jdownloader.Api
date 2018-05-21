using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class Extensions
    {
        private readonly JDownloaderApiHandler _ApiHandler;

        internal Extensions(JDownloaderApiHandler apiHandler)
        {
            _ApiHandler = apiHandler;
        }

        /// <summary>
        /// Installs an extension to the client.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <param name="extensionId">The id of the extension you want to install</param>
        /// <returns>True if successfull</returns>
        public bool Install(DeviceObject device, string extensionId)
        {
            var param = new[] {extensionId};
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extensions/install",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }

        /// <summary>
        /// Checks if the extension is enabled.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="className">Name/id of the extension.</param>
        /// <returns>True if enabled.</returns>
        public bool IsEnabled(DeviceObject device, string className)
        {
            var param = new[] { className };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extensions/isEnabled",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }

        /// <summary>
        /// Checks if the extension is installed.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="extensionId">The id of the extension you want to install.</param>
        /// <returns>True if successfull</returns>
        public bool IsInstalled(DeviceObject device, string extensionId)
        {
            var param = new[] { extensionId };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extensions/isInstalled",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }

        /// <summary>
        /// Gets all extensions that are available.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="requestObject">The request object which contains informations about which properties are returned.</param>
        /// <returns>A list of all extensions that are available.</returns>
        public ExtensionResponseObject[] List(DeviceObject device, ExtensionRequestObject requestObject)
        {
            string json = JsonConvert.SerializeObject(requestObject);
            var param = new[] { json };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extensions/list",
                param, JDownloaderHandler.LoginObject, true);

            JArray tmp = (JArray)response.Data;

            return tmp.ToObject <ExtensionResponseObject[]>();

        }

        /// <summary>
        /// Enableds or disables an extension
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="className">Name/id of the extension.</param>
        /// <param name="enabled">If true the extension gets enabled else it disables it.</param>
        /// <returns>True if successfull</returns>
        public bool SetEnabled(DeviceObject device, string className, bool enabled)
        {
            var param = new[] { className, enabled.ToString() };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/extensions/setEnabled",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null;
        }
    }
}
