
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class JD
    {
        private readonly JDownloaderApiHandler _ApiHandler;
        private readonly DeviceObject _Device;

        internal JD(JDownloaderApiHandler apiHandler, DeviceObject device)
        {
            _ApiHandler = apiHandler;
            _Device = device;
        }

        /// <summary>
        /// Keep an eye on your JDownloader Client ;)
        /// </summary>
        public void DoSomethingCool()
        {
            var response = _ApiHandler.CallAction<object>(_Device, "/jd/doSomethingCool",
                null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Gets the core revision of the jdownloader client.
        /// </summary>
        /// <returns>Returns the core revision of the jdownloader client.</returns>
        public int GetCoreRevision()
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/jd/getCoreRevision",
                null, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return -1;

            return (int)response.Data;
        }

        /// <summary>
        /// Refreshes the plugins.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public bool RefreshPlugins()
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/jd/refreshPlugins",
                null, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return false;

            return (bool)response.Data;
        }

        /// <summary>
        /// Creates the sum of two numbers.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns>Returns the sum of two numbers.</returns>
        public int Sum(int a, int b)
        {
            var param = new[] {a, b};
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/jd/sum",
                param, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return -1;

            return (int)response.Data;
        }

        /// <summary>
        /// Gets the current uptime of the JDownloader client.
        /// </summary>
        /// <returns>The current uptime of the JDownloader client as long.</returns>
        public long Uptime()
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/jd/uptime",
                null, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return -1;

            return (long)response.Data;
        }

        /// <summary>
        /// Gets the version of the JDownloader client.
        /// </summary>
        /// <returns>The current version of the JDownloader client.</returns>
        public long Version()
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/jd/version",
                null, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return -1;

            return (long)response.Data;
        }
    }
}
