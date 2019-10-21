using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.DownloadsV2;
using My.JDownloader.Api.Models.DownloadsV2.Request;
using My.JDownloader.Api.Models.DownloadsV2.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class DownloadsV2 : Base
    {

        internal DownloadsV2(JDownloaderApiHandler apiHandler, Device device)
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
            var response = ApiHandler.CallAction<DefaultResponse<long>>(Device, "/linkgrabberv2/getStopMark", null, JDownloaderHandler.LoginObject);
         
            if (response?.Data != null)
                return response.Data;
            return -1;
        }

        /// <summary>
        /// Gets informations about a stop marked link.
        /// </summary>
        /// <returns>Returns informations about a stop marked link.</returns>
        public StopMarkedLinkResponse GetStopMarkedLink()
        {
            var response = ApiHandler.CallAction<DefaultResponse<StopMarkedLinkResponse>>(Device, "/linkgrabberv2/getStopMark", null, JDownloaderHandler.LoginObject);

            return response?.Data;
        }

        /// <summary>
        /// Gets all entires that are currently in the download list.
        /// </summary>
        /// <param name="linkQuery">An object which allows you to filter the return object.</param>
        /// <returns>An enumerable of the DownloadLink which contains infos about the entries.</returns>
        public IEnumerable<DownloadLinkResponse> QueryLinks(LinkQueryRequest linkQuery)
        {
            string json = JsonConvert.SerializeObject(linkQuery);
            var param = new[] { json };

            var response =
                ApiHandler.CallAction<DefaultResponse<IEnumerable<DownloadLinkResponse>>>(Device, "/downloadsV2/queryLinks", param,
                    JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Gets all packages that are currently in the download list.
        /// </summary>
        /// <param name="linkQuery">An object which allows you to filter the return object.</param>
        /// <returns>An enumerable of the FilePackageResponse which contains infos about the packages.</returns>
        public IEnumerable<FilePackageResponse> QueryPackages(LinkQueryRequest linkQuery)
        {
            string json = JsonConvert.SerializeObject(linkQuery);
            var param = new[] { json };

            var response =
                ApiHandler.CallAction<DefaultResponse<IEnumerable<FilePackageResponse>>>(Device, "/downloadsV2/queryPackages", param,
                    JDownloaderHandler.LoginObject, true);
            return response?.Data;
        }
    }
}
