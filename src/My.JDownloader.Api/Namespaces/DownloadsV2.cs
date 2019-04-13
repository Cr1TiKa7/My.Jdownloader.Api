using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.DownloadsV2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        /// <summary>
        /// Gets all entires that are currently in the download list.
        /// </summary>
        /// <param name="linkQuery">An object which allows you to filter the return object.</param>
        /// <returns>An enumerable of the DownloadLinkObject which contains infos about the entries.</returns>
        public IEnumerable<DownloadLinkObject> QueryLinks(LinkQueryObject linkQuery)
        {
            string json = JsonConvert.SerializeObject(linkQuery);
            var param = new[] { json };

            var response =
                ApiHandler.CallAction<DefaultReturnObject>(Device, "/downloadsV2/queryLinks", param,
                    JDownloaderHandler.LoginObject, true);
            var tmp = (JArray)response?.Data;
            return tmp?.ToObject<IEnumerable<DownloadLinkObject>>();
        }

        /// <summary>
        /// Gets all packages that are currently in the download list.
        /// </summary>
        /// <param name="linkQuery">An object which allows you to filter the return object.</param>
        /// <returns>An enumerable of the FilePackageObject which contains infos about the packages.</returns>
        public IEnumerable<FilePackageObject> QueryPackages(LinkQueryObject linkQuery)
        {
            string json = JsonConvert.SerializeObject(linkQuery);
            var param = new[] { json };

            var response =
                ApiHandler.CallAction<DefaultReturnObject>(Device, "/downloadsV2/queryPackages", param,
                    JDownloaderHandler.LoginObject, true);
            var tmp = (JArray)response?.Data;
            return tmp?.ToObject<IEnumerable<FilePackageObject>>();
        }
    }
}
