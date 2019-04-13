using System.Collections.Generic;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Login;
using My.JDownloader.Api.Namespaces;
using Newtonsoft.Json.Linq;
using Extensions = My.JDownloader.Api.Namespaces.Extensions;

namespace My.JDownloader.Api
{
    public class DeviceHandler
    {
        private readonly DeviceObject _device;
        private readonly JDownloaderApiHandler _apiHandler;
        
        private LoginObject _loginObject;

        private byte[] _loginSecret;
        private byte[] _deviceSecret;

        public bool IsConnected;

        public Accounts Accounts;
        public AccountsV2 AccountsV2;
        public Captcha Captcha;
        public CaptchaForward CaptchaForward;
        public Config Config;
        public Dialogs Dialogs;
        public DownloadController DownloadController;
        public DownloadsV2 DownloadsV2;
        public Extensions Extensions;
        public Extraction Extraction;
        public LinkCrawler LinkCrawler;
        public LinkGrabberV2 LinkgrabberV2;
        public Update Update;
        public Jd Jd;
        public Namespaces.System System;

        internal DeviceHandler(DeviceObject device, JDownloaderApiHandler apiHandler, LoginObject loginObject, bool useJdownloaderApi = false)
        {
            _device = device;
            _apiHandler = apiHandler;
            _loginObject = loginObject;

            Accounts = new Accounts(_apiHandler, _device);
            AccountsV2 = new AccountsV2(_apiHandler, _device);
            Captcha = new Captcha(_apiHandler, _device);
            CaptchaForward = new CaptchaForward(_apiHandler, _device);
            Config = new Config(_apiHandler, _device);
            Dialogs = new Dialogs(_apiHandler, _device);
            DownloadController = new DownloadController(_apiHandler, _device);
            DownloadsV2 = new DownloadsV2(_apiHandler, _device);
            Extensions = new Extensions(_apiHandler, _device);
            Extraction = new Extraction(_apiHandler, _device);
            LinkCrawler = new LinkCrawler(_apiHandler, _device);
            LinkgrabberV2 = new LinkGrabberV2(_apiHandler, _device);
            Update = new Update(_apiHandler, _device);
            Jd = new Jd(_apiHandler, _device);
            System = new Namespaces.System(_apiHandler, _device);
            DirectConnect(useJdownloaderApi);
        }

        /// <summary>
        /// Tries to directly connect to the JDownloader Client.
        /// </summary>
        private void DirectConnect(bool useJdownloaderApi)
        {
            bool connected = false;
            if (useJdownloaderApi)
            {
                Connect("http://api.jdownloader.org");
                return;
            }
            foreach (var conInfos in GetDirectConnectionInfos())
            {
                if (Connect(string.Concat("http://", conInfos.Ip, ":", conInfos.Port)))
                {
                    connected = true;
                    break;
                }
            }
            if (connected == false )
                Connect("http://api.jdownloader.org");
        }


        private bool Connect(string apiUrl)
        {
            //Calculating the Login and Device secret
            _loginSecret = Utils.GetSecret(_loginObject.Email, _loginObject.Password, Utils.ServerDomain);
            _deviceSecret = Utils.GetSecret(_loginObject.Email, _loginObject.Password, Utils.DeviceDomain);

            //Creating the query for the connection request
            string connectQueryUrl =
                $"/my/connect?email={HttpUtility.UrlEncode(_loginObject.Email)}&appkey={HttpUtility.UrlEncode(Utils.AppKey)}";
            _apiHandler.SetApiUrl(apiUrl);
            //Calling the query
            var response = _apiHandler.CallServer<LoginObject>(connectQueryUrl, _loginSecret);

            //If the response is null the connection was not successful
            if (response == null)
                return false;

            response.Email = _loginObject.Email;
            response.Password = _loginObject.Password;

            //Else we are saving the response which contains the SessionToken, RegainToken and the RequestId
            _loginObject = response;
            _loginObject.ServerEncryptionToken = Utils.UpdateEncryptionToken(_loginSecret, _loginObject.SessionToken);
            _loginObject.DeviceEncryptionToken = Utils.UpdateEncryptionToken(_deviceSecret, _loginObject.SessionToken);
            IsConnected = true;
            return true;
        }

        private List<DeviceConnectionInfoObject> GetDirectConnectionInfos()
        {
            var tmp = _apiHandler.CallAction<DefaultReturnObject>(_device, "/device/getDirectConnectionInfos",
                null, _loginObject, true);
            if (tmp.Data == null || string.IsNullOrEmpty(tmp.Data.ToString()))
                return new List<DeviceConnectionInfoObject>();

            var jobj = (JObject) tmp.Data;
            var deviceConInfos = jobj.ToObject<DeviceConnectionInfoReturnObject>();

            return deviceConInfos.Infos;
        }
    }
}
