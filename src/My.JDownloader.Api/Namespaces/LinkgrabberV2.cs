using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.LinkgrabberV2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class LinkGrabberV2:Base
    {

    internal LinkGrabberV2(JDownloaderApiHandler apiHandler, DeviceObject device)
    {
        ApiHandler = apiHandler;
        Device = device;
    }

    /// <summary>
    /// Aborts the linkgrabber process.
    /// </summary>
    /// <returns>True if successfull.</returns>
    public bool Abort()
    {
        return Abort(-1);
    }

    /// <summary>
    /// Aborts the linkgrabber process for a specific job.
    /// </summary>
    /// <param name="jobId">The jobId you wnat to abort.</param>
    /// <returns>True if successfull.</returns>
    public bool Abort(long jobId)
    {
        var param = new[] {jobId};
        if (jobId == -1)
            param = null;

        var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/abort",
            param, JDownloaderHandler.LoginObject, true);

        if (response?.Data == null) return false;

        return (bool) response.Data;
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

        var json = JsonConvert.SerializeObject(containerObject);
        var param = new[] {json};
        var response = ApiHandler.CallAction<object>(Device, "/linkgrabberv2/addContainer",
            param, JDownloaderHandler.LoginObject);
        return response != null;
    }

    /// <summary>
    /// Adds a download link to the given device.
    /// </summary>
    /// <param name="requestObject">Contains informations like the link itself or the priority. If you want to use multiple links sperate them with an ';' char.</param>
    public bool AddLinks(AddLinkRequestObject requestObject)
    {
        requestObject.Links =  requestObject.Links.Replace(";", "\\r\\n");
        var json = JsonConvert.SerializeObject(requestObject);
        var param = new[] {json};
        var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/addLinks",
            param, JDownloaderHandler.LoginObject, true);
        return response != null;
    }

    /// <summary>
    /// Adds a variant copy of the link.
    /// </summary>
    /// <param name="linkId">The link id you want to copy.</param>
    /// <param name="destinationAfterLinkId"></param>
    /// <param name="destinationPackageId"></param>
    /// <param name="variantId"></param>
    /// <returns>True if successfull.</returns>
    public bool AddVariantCopy(long linkId, long destinationAfterLinkId, long destinationPackageId,
        string variantId)
    {
        var param = new[]
            {linkId.ToString(), destinationAfterLinkId.ToString(), destinationPackageId.ToString(), variantId};
        var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/addVariantCopy",
            param, JDownloaderHandler.LoginObject, true);
        return response != null;
    }

    /// <summary>
    /// Cleans up the downloader list.
    /// </summary>
    /// <param name="linkIds">Ids of the link you may want to clear.</param>
    /// <param name="packageIds">Ids of the packages you may want to clear.</param>
    /// <param name="action">The action type.</param>
    /// <param name="mode">The mode type.</param>
    /// <param name="selection">The selection Type.</param>
    /// <returns>True if successfull.</returns>
    public bool CleanUp(long[] linkIds, long[] packageIds, CleanUpActionType action, CleanUpModeType mode,
        CleanUpSelectionType selection)
    {
        var param = new object[] {linkIds, packageIds, action, mode, selection};
        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/cleanUp", param,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Clears the downloader list.
    /// </summary>
    /// <returns>True if successfull</returns>
    public bool ClearList()
    {
        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/clearList", null,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Not documented what it really does.
    /// </summary>
    /// <param name="structureWatermark"></param>
    /// <returns></returns>
    public long GetChildrenChanged(long structureWatermark)
    {
        var response =
            ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/getChildrenChanged", null,
                JDownloaderHandler.LoginObject);

        if (response?.Data != null)
            return (long) response.Data;
        return -1;
    }

    /// <summary>
    /// Gets the selection base of the download folder history.
    /// </summary>
    /// <returns>An array which contains the download folder history.</returns>
    public string[] GetDownloadFolderHistorySelectionBase()
    {
        var response = ApiHandler.CallAction<DefaultReturnObject>(Device,
            "/linkgrabberv2/getDownloadFolderHistorySelectionBase", null, JDownloaderHandler.LoginObject);

        var tmp = (JArray) response?.Data;
        return tmp?.ToObject<string[]>();
    }

    // TODO: Describe what this function does.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="links"></param>
    /// <param name="afterLinkId"></param>
    /// <param name="destPackageId"></param>
    /// <returns></returns>
    public Dictionary<string, List<long>> GetDownloadUrls(long[] links, long afterLinkId, long destPackageId)
    {
        var response = ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/getDownloadUrls", null,
            JDownloaderHandler.LoginObject);

        var tmp = (JObject) response?.Data;
        return tmp?.ToObject<Dictionary<string, List<long>>>();
    }

    /// <summary>
    /// Checks how many packages are inside the linkcollector.
    /// </summary>
    /// <returns>The amount of links which are in the linkcollector.</returns>
    public int GetPackageCount()
    {
        var response =
            ApiHandler.CallAction<dynamic>(Device, "/linkgrabberv2/getPackageCount", null,
                JDownloaderHandler.LoginObject, true);
        if (response == null)
            return 0;
        return response.data;
    }

    /// <summary>
    /// Gets variants of the link.
    /// </summary>
    /// <param name="linkId">The link id you want to get the variants of.</param>
    /// <returns>An enumerable which contains variants of the given link.</returns>
    public IEnumerable<GetVariantsReturnObject> GetVariants(long linkId)
    {
        var response =
            ApiHandler.CallAction<DefaultReturnObject>(Device, "/linkgrabberv2/getVariants", null,
                JDownloaderHandler.LoginObject);

        var tmp = (JArray) response?.Data;
        return tmp?.ToObject<IEnumerable<GetVariantsReturnObject>>();
    }

    /// <summary>
    /// Checks if the JDownloader client is still collecting files from links.
    /// </summary>
    /// <returns>Returns true or false. Depending on if the client is still collecting files.</returns>
    public bool IsCollecting()
    {
        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/isCollection", null,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Moves one or multiple links after another link or inside a package.
    /// </summary>
    /// <param name="linkIds">The ids of the links you want to move.</param>
    /// <param name="afterLinkId">The id of the link you want to move the other links to.</param>
    /// <param name="destPackageId">The id of the package where you want to add the links to.</param>
    /// <returns>True if successfull.</returns>
    public bool MoveLinks(long[] linkIds, long afterLinkId, long destPackageId)
    {
        var param = new object[] {linkIds, afterLinkId, destPackageId};

        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/moveLinks", param,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Moves one or multiple packages after antoher package.
    /// </summary>
    /// <param name="packageIds">The ids of the packages you want to move.</param>
    /// <param name="afterDestPackageId">The id of the package you want to move the others to.</param>
    /// <returns>True if successfull.</returns>
    public bool MovePackages(long[] packageIds, long afterDestPackageId)
    {
        var param = new object[] {packageIds, afterDestPackageId};

        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/movePackages", param,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Moves one or multiple links/packages to the download list.
    /// </summary>
    /// <param name="linkIds">The ids of the links you want to move.</param>
    /// <param name="packageIds">The ids of the packages you want to move.</param>
    /// <returns>True if successfull.</returns>
    public bool MoveToDownloadlist(long[] linkIds, long[] packageIds)
    {
        var param = new[] {linkIds, packageIds};

        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/moveToDownloadlist", param,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Move one or multiple links/packages to a new package.
    /// </summary>
    /// <param name="linkIds">The ids of the links you want to move.</param>
    /// <param name="packageIds">The ids of the packages you want to move.</param>
    /// <param name="newPackageName">The name of the new package.</param>
    /// <param name="downloadPath">The download path.</param>
    /// <returns>True if successfull.</returns>
    public bool MoveToNewPackage(long[] linkIds, long[] packageIds, string newPackageName, string downloadPath)
    {
        var param = new object[] {linkIds, packageIds, newPackageName, downloadPath};

        var response =
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/moveToNewPackage", param,
                JDownloaderHandler.LoginObject);
        if (response == null)
            return false;
        return true;
    }

    /// <summary>
    /// Gets all links that are currently in the linkcollector.
    /// </summary>
    /// <param name="maxResults">Maximum number of return values.</param>
    /// <returns>An enumerable of all links that are currently in the linkcollector list.</returns>
    public IEnumerable<QueryLinksResponseObject> QueryLinks(int maxResults = -1)
    {
        QueryLinksObject queryLink = new QueryLinksObject
        {
            Availability = true,
            Url = true
        };
        if (maxResults > 0)
            queryLink.MaxResults = maxResults;

        string json = JsonConvert.SerializeObject(queryLink);
        var param = new[] {json};

        var response =
            ApiHandler.CallAction<CrawledLinkObject>(Device, "/linkgrabberv2/queryLinks", param,
                JDownloaderHandler.LoginObject, true);
        return response?.Data;
    }

    /// <summary>
    /// Gets a list of available packages that are currently in the linkcollector.
    /// </summary>
    /// <param name="requestObject">The request object which contains properties to define the return properties.</param>
    /// <returns>An enumerable of all available packages.</returns>
    public IEnumerable<QueryPackagesResponseObject> QueryPackages(QueryPackagesRequestObject requestObject)
    {
        string json = JsonConvert.SerializeObject(requestObject);
        var param = new[] {json};

        var response =
            ApiHandler.CallAction<QueryPackagesObject>(Device, "/linkgrabberv2/queryPackages", param,
                JDownloaderHandler.LoginObject, true);
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
            ApiHandler.CallAction<object>(Device, "/linkgrabberv2/setDownloadDirectory", param,
                JDownloaderHandler.LoginObject, true);
        return response != null;
    }
    }
}