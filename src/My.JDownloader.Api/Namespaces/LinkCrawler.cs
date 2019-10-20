using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public class LinkCrawler : Base
    {

        internal LinkCrawler(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Asks the client if the linkcrawler is still crawling.
        /// </summary>
        /// <returns>Ture if succesfull</returns>
        public bool IsCrawling()
        {
            var response =
                ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/linkcrawler/isCrawling", null, JDownloaderHandler.LoginObject);
            if (response?.Data == null)
                return false;

            return response.Data;
        }
    }
}
