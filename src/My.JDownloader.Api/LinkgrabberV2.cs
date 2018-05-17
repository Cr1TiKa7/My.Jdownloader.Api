using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.LinkgrabberV2;
using Newtonsoft.Json;

namespace My.JDownloader.Api
{
    public class LinkgrabberV2
    {
        private readonly JDownloaderApiHandler _ApiHandler;

        internal LinkgrabberV2(JDownloaderApiHandler apiHandler)
        {
            _ApiHandler = apiHandler;
        }

        /// <summary>
        /// Adds a download link to the given device.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <param name="links">The downloadlinks. Seperated by a space.</param>
        /// <param name="packageName">The name of the package.</param>
        /// <param name="priority">The priority of the download. Can be one of the following: HIGHEST, HIGHER, HIGH, DEFAULT, LOW, LOWER, LOWEST</param>
        /// <param name="downloadPassword">The password which may be needed for a download.</param>
        /// <param name="extractPassword">The password if the archive which will be downloaded is locked with.</param>
        /// <param name="autoStart">If true the download starts automatically.</param>
        /// <param name="autoExtract">If true it extracts the downloaded archive after finishing the download.</param>
        public void AddLinks(DeviceObject device, string links, string packageName, string destinationFolder, string priority = "DEFAULT", string extractPassword = "", string downloadPassword = "", bool autoStart = true, bool autoExtract = true)
        {
            AddLinkObject linkObject = new AddLinkObject
            {
                Priority = priority,
                Links = links.Replace(" ", "\\r\\n"),
                AutoStart = autoStart,
                PackageName = packageName,
                AutoExtract = autoExtract,
                DownloadPassword = downloadPassword,
                ExtractPassword = extractPassword,
                DestinationFolder = destinationFolder
            };
            string json = JsonConvert.SerializeObject(linkObject);
            var param = new[] { json };
            var response = _ApiHandler.CallAction<object>(device, "/linkgrabberv2/addLinks",
                param, JDownloaderHandler.LoginObject);
        }

        /// <summary>
        /// Adds a container to the linkcollector list.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <param name="type">The value can be: DLC, RSDF, CCF or CRAWLJOB</param>
        /// <param name="content">File as dataurl. https://de.wikipedia.org/wiki/Data-URL </param>
        public void AddContainer(DeviceObject device, ContainerType type, string content)
        {
            AddContainerObject containerObject = new AddContainerObject
            {
                Type = type.ToString(),
                Content = content
            };

            string json = JsonConvert.SerializeObject(containerObject);
            var param = new[] { json };
            var response = _ApiHandler.CallAction<object>(device, "/linkgrabberv2/addContainer",
                param, JDownloaderHandler.LoginObject);
        }

        /// <summary>
        /// Clears the linkcollector list.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>True if successfull</returns>
        public bool ClearList(DeviceObject device)
        {
            var response =
                _ApiHandler.CallAction<object>(device, "/linkgrabberv2/clearList", null, JDownloaderHandler.LoginObject);
            if (response == null)
                return false;
            return true;
        }

        /// <summary>
        /// Checks how many packages are inside the linkcollector.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>The amount of links which are in the linkcollector.</returns>
        public int GetPackageCount(DeviceObject device)
        {
            var response =
                _ApiHandler.CallAction<dynamic>(device, "/linkgrabberv2/getPackageCount", null, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return 0;
            return response.data;
        }

        /// <summary>
        /// Checks if the JDownloader client is still collecting files from links.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>Returns true or false. Depending on if the client is still collecting files.</returns>
        public bool IsCollecting(DeviceObject device)
        {
            var response =
                _ApiHandler.CallAction<object>(device, "/linkgrabberv2/isCollection", null, JDownloaderHandler.LoginObject);
            if (response == null)
                return false;
            return true;
        }

        public List<CrawledLinkDataObject> QueryLinks(DeviceObject device, int maxResults = -1)
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
                _ApiHandler.CallAction<CrawledLinkObject>(device, "/linkgrabberv2/queryLinks", param, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return null;
            return response.Data;
        }
    }
}
