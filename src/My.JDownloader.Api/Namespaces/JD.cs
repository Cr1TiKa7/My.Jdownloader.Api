using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class Jd:Base
    {

        internal Jd(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Keep an eye on your JDownloader Client ;)
        /// </summary>
        public void DoSomethingCool()
        {
            ApiHandler.CallAction<object>(Device, "/jd/doSomethingCool",
                null, JDownloaderHandler.LoginObject, true);
        }

        /// <summary>
        /// Gets the core revision of the jdownloader client.
        /// </summary>
        /// <returns>Returns the core revision of the jdownloader client.</returns>
        public int GetCoreRevision()
        {
            var response = ApiHandler.CallAction<DefaultResponse<int>>(Device, "/jd/getCoreRevision",
                null, JDownloaderHandler.LoginObject, true);
            if (response != null)
                return response.Data;

            return -1;
        }

        /// <summary>
        /// Refreshes the plugins.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public bool RefreshPlugins()
        {
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/jd/refreshPlugins",
                null, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data;
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
            var response = ApiHandler.CallAction<DefaultResponse<int>>(Device, "/jd/sum",
                param, JDownloaderHandler.LoginObject, true);

            if (response != null)
                return response.Data;

            return -1;
        }

        /// <summary>
        /// Gets the current uptime of the JDownloader client.
        /// </summary>
        /// <returns>The current uptime of the JDownloader client as long.</returns>
        public long Uptime()
        {
            var response = ApiHandler.CallAction<DefaultResponse<long>>(Device, "/jd/uptime",
                null, JDownloaderHandler.LoginObject, true);

            if (response != null)
                return response.Data;

            return -1;
        }

        /// <summary>
        /// Gets the version of the JDownloader client.
        /// </summary>
        /// <returns>The current version of the JDownloader client.</returns>
        public long Version()
        {
            var response = ApiHandler.CallAction<DefaultResponse<long>>(Device, "/jd/version",
                null, JDownloaderHandler.LoginObject, true);

            if (response != null) 
                return response.Data;

            return -1;
        }
    }
}
