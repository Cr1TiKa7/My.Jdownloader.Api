using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.System;
using My.JDownloader.Api.Models.System.Response;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class System : Base
    {

        internal System(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Closes the JDownloader client.
        /// </summary>
        public void ExitJd()
        {
            ApiHandler.CallAction<object>(Device, "/system/exitJD", null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Gets storage informations of the given path.
        /// </summary>
        /// <param name="path">The Path you want to check.</param>
        /// <returns>An array with storage informations.</returns>
        public StorageInfoResponse[] GetStorageInfos(string path)
        {
            var param = new[] {path};
            var response = ApiHandler.CallAction<DefaultResponse<StorageInfoResponse[]>>(Device, "/system/getStorageInfos", param, JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Gets information of the system the JDownloader client is running on.
        /// </summary>
        /// <returns></returns>
        public SystemInfoResponse GetSystemInfos()
        {
            var response = ApiHandler.CallAction<DefaultResponse<SystemInfoResponse>>(Device, "/system/getSystemInfos", null, JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Hibernates the current os the JDownloader client is running on.
        /// </summary>
        public void HibernateOs()
        {
            ApiHandler.CallAction<object>(Device, "/system/hibernateOS", null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Restarts the JDownloader client.
        /// </summary>
        public void RestartJd()
        {
            ApiHandler.CallAction<object>(Device, "/system/restartJD", null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Shutsdown the current os the JDownloader client is running on.
        /// </summary>
        /// <param name="force">True if you want to force the shutdown process.</param>
        public void ShutdownOs(bool force)
        {
            ApiHandler.CallAction<object>(Device, "/system/shutdownOS", new [] {force}, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Sets the current os the JDownloader client is running on in standby.
        /// </summary>
        public void StandbyOs()
        {
            ApiHandler.CallAction<object>(Device, "/system/standbyOS", null, JDownloaderHandler.LoginObject, true);
        }
    }
}
