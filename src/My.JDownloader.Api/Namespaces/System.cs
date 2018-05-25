using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;

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


        public List<object> GetStorageInfos(string path)
        {
            var param = new[] {path};
            var tmp =_ApiHandler.CallAction<DefaultReturnObject>(_Device, "/system/getStorageInfos", param, JDownloaderHandler.LoginObject, true);

            return new List<object>();
        }


        /// <summary>
        /// Restarts the JDownloader client.
        /// </summary>
        public void RestartJd()
        {
            _ApiHandler.CallAction<object>(_Device, "/system/restartJD", null, JDownloaderHandler.LoginObject, true);
        }
    }
}
