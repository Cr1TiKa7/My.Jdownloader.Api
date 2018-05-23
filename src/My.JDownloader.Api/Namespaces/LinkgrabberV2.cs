using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.LinkgrabberV2;
using Newtonsoft.Json;

namespace My.JDownloader.Api.Namespaces
{
    public class LinkgrabberV2
    {
        private readonly JDownloaderApiHandler _ApiHandler;
        private readonly DeviceObject _Device;

        internal LinkgrabberV2(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            _ApiHandler = apiHandler;
            _Device = device;
        }

        /// <summary>
        /// Adds a download link to the given device.
        /// </summary>
        /// <param name="requestObject">Contains informations like the link itself or the priority. If you want to use multiple links sperate them with an ';' char.</param>
        public bool AddLinks(AddLinkRequestObject requestObject)
        {
            requestObject.Links.Replace(";", "\\r\\n");
            string json = JsonConvert.SerializeObject(requestObject);
            var param = new[] { json };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/linkgrabberv2/addLinks",
                param, JDownloaderHandler.LoginObject,true);
            return response != null;
        }

        /// <summary>
        /// Adds a container to the linkcollector list.
        /// </summary>
        /// <param name="type">The value can be: DLC, RSDF, CCF or CRAWLJOB</param>
        /// <param name="content">File as dataurl. https://de.wikipedia.org/wiki/Data-URL </param>
        public bool AddContainer(ContainerType type, string content)
        {
            AddContainerObject containerObject = new AddContainerObject
            {
                Type = type.ToString(),
                Content = content
            };

            string json = JsonConvert.SerializeObject(containerObject);
            var param = new[] { json };
            var response = _ApiHandler.CallAction<object>(_Device, "/linkgrabberv2/addContainer",
                param, JDownloaderHandler.LoginObject);
            return response != null;
        }

        /// <summary>
        /// Clears the linkcollector list.
        /// </summary>
        /// <returns>True if successfull</returns>
        public bool ClearList()
        {
            var response =
                _ApiHandler.CallAction<object>(_Device, "/linkgrabberv2/clearList", null, JDownloaderHandler.LoginObject);
            if (response == null)
                return false;
            return true;
        }

        /// <summary>
        /// Checks how many packages are inside the linkcollector.
        /// </summary>
        /// <returns>The amount of links which are in the linkcollector.</returns>
        public int GetPackageCount()
        {
            var response =
                _ApiHandler.CallAction<dynamic>(_Device, "/linkgrabberv2/getPackageCount", null, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return 0;
            return response.data;
        }

        /// <summary>
        /// Checks if the JDownloader client is still collecting files from links.
        /// </summary>
        /// <returns>Returns true or false. Depending on if the client is still collecting files.</returns>
        public bool IsCollecting()
        {
            var response =
                _ApiHandler.CallAction<object>(_Device, "/linkgrabberv2/isCollection", null, JDownloaderHandler.LoginObject);
            if (response == null)
                return false;
            return true;
        }

        /// <summary>
        /// Gets all links that are currently in the linkcollector.
        /// </summary>
        /// <param name="maxResults">Maximum number of return values.</param>
        /// <returns>Returns a list of all links that are currently in the linkcollector list.</returns>
        public List<QueryLinksResponseObject> QueryLinks( int maxResults = -1)
        {
            QueryLinksObject queryLink = new QueryLinksObject
            {
                Availability = true,
                Url = true
            };
            if (maxResults > 0)
                queryLink.MaxResults = maxResults;

            string json = JsonConvert.SerializeObject(queryLink);
            var param = new[] { json };

            var response =
                _ApiHandler.CallAction<CrawledLinkObject>(_Device, "/linkgrabberv2/queryLinks", param, JDownloaderHandler.LoginObject, true);
            return response?.Data;
        }

        /// <summary>
        /// Gets a list of available packages that are currently in the linkcollector.
        /// </summary>
        /// <param name="requestObject">The request object which contains properties to define the return properties.</param>
        /// <returns>Returns a list of all available packages.</returns>
        public List<QueryPackagesResponseObject> QueryPackages( QueryPackagesRequestObject requestObject)
        {
            string json = JsonConvert.SerializeObject(requestObject);
            var param = new[] {json};

            var response =
                _ApiHandler.CallAction<QueryPackagesObject>(_Device, "/linkgrabberv2/queryPackages", param, JDownloaderHandler.LoginObject, true);
            return response?.Data;
        }

        /// <summary>
        /// Allows you to change the download directory of multiple packages.
        /// </summary>
        /// <param name="directory">The new download directory.</param>
        /// <param name="packageIds">The ids of the packages.</param>
        /// <returns>True if successfull</returns>
        public bool SetDownloadDirectory(string directory, long[] packageIds)
        {
            var param = new object[] {directory, packageIds};
            var response =
                _ApiHandler.CallAction<object>(_Device, "/linkgrabberv2/setDownloadDirectory", param, JDownloaderHandler.LoginObject,true);
            return response != null;
        }
    }
}
