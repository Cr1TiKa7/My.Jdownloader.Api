using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models.Devices;

namespace My.JDownloader.Api.Namespaces
{
    public abstract class Base
    {
        internal Base() {}

        public JDownloaderApiHandler ApiHandler;
        public Device Device;
    }
}
