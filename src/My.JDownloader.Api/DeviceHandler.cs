using System.Collections.Generic;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.Login;
using My.JDownloader.Api.Namespaces;
using Newtonsoft.Json.Linq;
using Extensions = My.JDownloader.Api.Namespaces.Extensions;

namespace My.JDownloader.Api
{
    public class DeviceHandler
    {
        private DeviceObject _Device;
        private JDownloaderApiHandler _ApiHandler;
        
        private LoginObject _LoginObject;

        private byte[] _LoginSecret;
        private byte[] _DeviceSecret;

        public bool IsConnected;

        public AccountsV2 AccountsV2;
        public DownloadController DownloadController;
        public Extensions Extensions;
        public Extraction Extraction;
        public LinkgrabberV2 LinkgrabberV2;
        public Update Update;

        internal DeviceHandler(DeviceObject device, JDownloaderApiHandler apiHandler, LoginObject LoginObject)
        {
            _Device = device;
            _ApiHandler = apiHandler;
            _LoginObject = LoginObject;

            AccountsV2 = new AccountsV2(_ApiHandler, _Device);
            DownloadController = new DownloadController(_ApiHandler, _Device);
            Extensions = new Extensions(_ApiHandler, _Device);
            Extraction = new Extraction(_ApiHandler, _Device);
            LinkgrabberV2 = new LinkgrabberV2(_ApiHandler, _Device);
            Update = new Update(_ApiHandler, _Device);
        }

        /// <summary>
        /// Tries to directly connect to the JDownloader Client.
        /// </summary>
        /// <returns>True if successfull.</returns>
        public bool DirectConnect()
        {
            bool connected = false;
            foreach (var conInfos in GetDirectConnectionInfos())
            {
                if (Connect(string.Concat("http://", conInfos.Ip, ":", conInfos.Port)))
                {
                    connected = true;
                    break;
                }
            }
            if (connected == false)
                Connect("http://api.jdownloader.org");

            return connected;
        }


        private bool Connect(string apiUrl)
        {
            //Calculating the Login and Device secret
            _LoginSecret = Utils.GetSecret(_LoginObject.Email, _LoginObject.Password, Utils.ServerDomain);
            _DeviceSecret = Utils.GetSecret(_LoginObject.Email, _LoginObject.Password, Utils.DeviceDomain);

            //Creating the query for the connection request
            string connectQueryUrl =
                $"/my/connect?email={HttpUtility.UrlEncode(_LoginObject.Email)}&appkey={HttpUtility.UrlEncode(Utils.AppKey)}";

            //Calling the query
            var response = _ApiHandler.CallServer<LoginObject>(connectQueryUrl, _LoginSecret);

            //If the response is null the connection was not successfull
            if (response == null)
                return false;

            response.Email = _LoginObject.Email;
            response.Password = _LoginObject.Password;

            //Else we are saving the response which contains the SessionToken, RegainToken and the RequestId
            _LoginObject = response;
            _LoginObject.ServerEncryptionToken = Utils.UpdateEncryptionToken(_LoginSecret, _LoginObject.SessionToken);
            _LoginObject.DeviceEncryptionToken = Utils.UpdateEncryptionToken(_DeviceSecret, _LoginObject.SessionToken);
            IsConnected = true;
            return true;
        }

        private List<DeviceConnectionInfoObject> GetDirectConnectionInfos()
        {
            var tmp = _ApiHandler.CallAction<DefaultReturnObject>(_Device, "/device/getDirectConnectionInfos",
                null, _LoginObject, true);
            if (string.IsNullOrEmpty(tmp.Data.ToString()))
                return new List<DeviceConnectionInfoObject>();

            var jobj = (JObject) tmp.Data;
            var deviceConInfos = jobj.ToObject<DeviceConnectionInfoReturnObject>();

            return deviceConInfos.Infos;
        }
    }
}
