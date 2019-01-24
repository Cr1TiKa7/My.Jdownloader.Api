using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class Extensions : Base
    {

        internal Extensions(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Installs an extension to the client.
        /// </summary>
        /// <param name="extensionId">The id of the extension you want to install</param>
        /// <returns>True if successfull</returns>
        public bool Install( string extensionId)
        {
            var param = new[] {extensionId};
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/extensions/install",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }

        /// <summary>
        /// Checks if the extension is enabled.
        /// </summary>
        /// <param name="className">Name/id of the extension.</param>
        /// <returns>True if enabled.</returns>
        public bool IsEnabled(string className)
        {
            var param = new[] { className };
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/extensions/isEnabled",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }

        /// <summary>
        /// Checks if the extension is installed.
        /// </summary>
        /// <param name="extensionId">The id of the extension you want to install.</param>
        /// <returns>True if successfull</returns>
        public bool IsInstalled( string extensionId)
        {
            var param = new[] { extensionId };
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/extensions/isInstalled",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && (bool)response.Data;
        }

        /// <summary>
        /// Gets all extensions that are available.
        /// </summary>
        /// <param name="requestObject">The request object which contains informations about which properties are returned.</param>
        /// <returns>An enumerable of all extensions that are available.</returns>
        public IEnumerable<ExtensionResponseObject> List( ExtensionRequestObject requestObject)
        {
            string json = JsonConvert.SerializeObject(requestObject);
            var param = new[] { json };
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/extensions/list",
                param, JDownloaderHandler.LoginObject, true);

            JArray tmp = (JArray)response.Data;

            return tmp.ToObject<IEnumerable<ExtensionResponseObject>>();

        }

        /// <summary>
        /// Enableds or disables an extension
        /// </summary>
        /// <param name="className">Name/id of the extension.</param>
        /// <param name="enabled">If true the extension gets enabled else it disables it.</param>
        /// <returns>True if successfull</returns>
        public bool SetEnabled(string className, bool enabled)
        {
            var param = new[] { className, enabled.ToString() };
            var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/extensions/setEnabled",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null;
        }
    }
}
