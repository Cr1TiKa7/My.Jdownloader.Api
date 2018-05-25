using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.System;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class System
    {
        private readonly JDownloaderApiHandler _ApiHandler;
        private readonly DeviceObject _Device;

        internal System(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            _ApiHandler = apiHandler;
            _Device = device;
        }

        /// <summary>
        /// Closes the JDownloader client.
        /// </summary>
        public void ExitJd()
        {
            _ApiHandler.CallAction<object>(_Device, "/system/exitJD", null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Gets storage informations of the given path.
        /// </summary>
        /// <param name="path">The Path you want to check.</param>
        /// <returns>An array with storage informations.</returns>
        public StorageInfoReturnObject[] GetStorageInfos(string path)
        {
            var param = new[] {path};
            var tmp =_ApiHandler.CallAction<DefaultReturnObject>(_Device, "/system/getStorageInfos", param, JDownloaderHandler.LoginObject, true);

            var data = (JArray) tmp?.Data;
            return data?.ToObject<StorageInfoReturnObject[]>();
        }

        /// <summary>
        /// Gets information of the system the JDownloader client is running on.
        /// </summary>
        /// <returns></returns>
        public SystemInfoReturnObject GetSystemInfos()
        {
            var tmp = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/system/getSystemInfos", null, JDownloaderHandler.LoginObject, true);

            var data = (JObject) tmp?.Data;
            return data?.ToObject<SystemInfoReturnObject>();
        }

        /// <summary>
        /// Hibernates the current os the JDownloader client is running on.
        /// </summary>
        public void HibernateOS()
        {
            _ApiHandler.CallAction<object>(_Device, "/system/hibernateOS", null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Restarts the JDownloader client.
        /// </summary>
        public void RestartJd()
        {
            _ApiHandler.CallAction<object>(_Device, "/system/restartJD", null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Shutsdown the current os the JDownloader client is running on.
        /// </summary>
        /// <param name="force">True if you want to force the shutdown process.</param>
        public void ShutdownOS(bool force)
        {
            _ApiHandler.CallAction<object>(_Device, "/system/shutdownOS", new [] {force}, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Sets the current os the JDownloader client is running on in standby.
        /// </summary>
        public void StandbyOS()
        {
            _ApiHandler.CallAction<object>(_Device, "/system/standbyOS", null, JDownloaderHandler.LoginObject, true);
        }
    }
}
