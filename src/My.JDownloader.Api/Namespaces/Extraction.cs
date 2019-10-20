using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Extraction;
using My.JDownloader.Api.Models.Extraction.Response;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class Extraction : Base
    {

        internal Extraction(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Adds an archive password to the client.
        /// </summary>
        /// <param name="password">The password to add.</param>
        /// <returns>True if successful.</returns>
        public bool AddArchivePassword(string password)
        {
            var param = new[] {password};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/extraction/addArchivePassword",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null;
        }

        /// <summary>
        /// Cancels an extraction process.
        /// </summary>
        /// <param name="controllerId">The id of the control you want to cancel.</param>
        /// <returns>True if successful.</returns>
        public bool CancelExtraction(string controllerId)
        {
            var param = new[] { controllerId };
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/extraction/cancelExtraction",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data;
        }

        /// <summary>
        /// Gets infos about the archive status.
        /// </summary>
        /// <param name="linkIds">Ids of the links you want to check.</param>
        /// <param name="packageIds">Ids of the packages you want to check.</param>
        /// <returns>An enumerable which contains all the archive statuses.</returns>
        public IEnumerable<ArchiveStatusResponse> GetArchiveInfo(long[] linkIds, long[] packageIds)
        {
            var param = new[] { linkIds,packageIds };
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/extraction/getArchiveInfo",
                param, JDownloaderHandler.LoginObject, true);

            JArray tmp = (JArray)response.Data;
            return tmp.ToObject<IEnumerable<ArchiveStatusResponse>>();
        }

        /// <summary>
        /// Gets the settings for the given archives.
        /// </summary>
        /// <param name="archiveIds">The archive ids you want the settings from.</param>
        /// <returns>An enumerable which contains the settings of the given archive ids.</returns>
        public IEnumerable<ArchiveSettingsResponse> GetArchiveSettings(string[] archiveIds)
        {
            var param = new[] { archiveIds };
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/extraction/getArchiveSettings",
                param, JDownloaderHandler.LoginObject, true);

            JArray tmp = (JArray)response.Data;
            return tmp.ToObject<IEnumerable<ArchiveSettingsResponse>>();
        }
        
        /// <summary>
        /// Gets all archives statuses that are currently queued.
        /// </summary>
        /// <returns>An enumerable which contains all archive statuses of the queued archives.</returns>
        public IEnumerable<ArchiveStatusResponse> GetQueue()
        {
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/extraction/getQueue",
                null, JDownloaderHandler.LoginObject, true);

            JArray tmp = (JArray)response.Data;
            return tmp.ToObject<IEnumerable<ArchiveStatusResponse>>();
        }

        /// <summary>
        /// Sets the settings for an archive.
        /// </summary>
        /// <param name="archiveId">The id of the archive you want to update the settings.</param>
        /// <param name="archiveSettings">The new settings for the archive.</param>
        /// <returns>True if successful.</returns>
        public bool SetArchiveSettings(string archiveId, ArchiveSettingsResponse archiveSettings)
        {
            var param = new object[] { archiveId, archiveSettings };
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/extraction/setArchiveSettings",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data;
        }

        /// <summary>
        /// Starts the extraction for specific packages and links.
        /// </summary>
        /// <param name="linkIds">The ids of the links you want to start the extraction.</param>
        /// <param name="packageIds">The ids of the packages you want to start the extraction.</param>
        /// <returns>A dictionary which contains the archive id as the key and the extraction status as value.</returns>
        public Dictionary<string,bool?> StartExtractionNow(long[] linkIds, long[] packageIds)
        {
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/extraction/startExtractionNow",
                null, JDownloaderHandler.LoginObject, true);

            var tmp = ((JObject)response.Data);
            if (tmp != null)
                return tmp.ToObject<Dictionary<string, bool?>>();

            return new Dictionary<string, bool?>();
        }
    }
}
