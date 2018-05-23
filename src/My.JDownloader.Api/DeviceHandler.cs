using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.Login;
using My.JDownloader.Api.Namespaces;

namespace My.JDownloader.Api
{
    public class DeviceHandler
    {
        private DeviceObject _Device;
        private JDownloaderApiHandler _ApiHandler;
        private LoginObject _LoginObject;

        public AccountsV2 AccountsV2;
        public DownloadController DownloadController;
        public Extensions Extensions;
        public Extraction Extraction;
        public LinkgrabberV2 LinkgrabberV2;
        public Update Update;

        internal DeviceHandler(DeviceObject device, JDownloaderApiHandler apiHandler, LoginObject loginObject)
        {
            _Device = device;
            _ApiHandler = apiHandler;
            _LoginObject = loginObject;

            AccountsV2 = new AccountsV2(_ApiHandler, _Device);
            DownloadController = new DownloadController(_ApiHandler, _Device);
            Extensions = new Extensions(_ApiHandler, _Device);
            Extraction = new Extraction(_ApiHandler, _Device);
            LinkgrabberV2 = new LinkgrabberV2(_ApiHandler, _Device);
            Update = new Update(_ApiHandler, _Device);
        }

        private void Connect(string email, string password)
        {
            //TODO: Try to connect to a local client. Else use the relay server.
        }
    }
}
