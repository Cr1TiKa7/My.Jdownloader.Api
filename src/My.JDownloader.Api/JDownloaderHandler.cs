using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.Login;
using My.JDownloader.Api.Namespaces;

namespace My.JDownloader.Api
{
    public class JDownloaderHandler
    {
        public bool IsConnected { get; set; } = false;

        internal static LoginObject LoginObject;

        private byte[] _LoginSecret;
        private byte[] _DeviceSecret;

        private const string _ServerDomain = "server";
        private const string _DeviceDomain = "device";
        private const string _AppKey = "my.jdownloader.api.wrapper";
        
        private readonly JDownloaderApiHandler _ApiHandler = new JDownloaderApiHandler();

        public AccountsV2 AccountsV2;
        public DownloadController DownloadController;
        public Extensions Extensions;
        public Extraction Extraction;
        public LinkgrabberV2 LinkgrabberV2;
        public Update Update;

        public JDownloaderHandler()
        {
            InitializeClasses();
        }

        public JDownloaderHandler(string email, string password)
        {
            Connect(email, password);
            InitializeClasses();
        }

        private void InitializeClasses()
        {
            //AccountsV2 = new AccountsV2(_ApiHandler);
            //DownloadController = new DownloadController(_ApiHandler);
            //Extensions = new Extensions(_ApiHandler);
            //Extraction = new Extraction(_ApiHandler);
            //LinkgrabberV2 = new LinkgrabberV2(_ApiHandler);
            //Update = new Update(_ApiHandler);
        }

        #region "Connection methods"

        /// <summary>
        /// Fires a connection request to the api.
        /// </summary>
        /// <param name="email">Email of the User</param>
        /// <param name="password">Password of the User</param>
        /// <returns>Return if the Connection was succesfull</returns>
        public bool Connect(string email, string password)
        {
            //Calculating the Login and Device secret
            _LoginSecret = GetSecret(email, password, _ServerDomain);
            _DeviceSecret = GetSecret(email, password, _DeviceDomain);

            //Creating the query for the connection request
            string connectQueryUrl =
                $"/my/connect?email={HttpUtility.UrlEncode(email)}&appkey={HttpUtility.UrlEncode(_AppKey)}";

            //Calling the query
            var response = _ApiHandler.CallServer<LoginObject>(connectQueryUrl, _LoginSecret);

            //If the response is null the connection was not successfull
            if (response == null)
                return false;

            //Else we are saving the response which contains the SessionToken, RegainToken and the RequestId
            LoginObject = response;
            LoginObject.ServerEncryptionToken = UpdateEncryptionToken(_LoginSecret, LoginObject.SessionToken);
            LoginObject.DeviceEncryptionToken = UpdateEncryptionToken(_DeviceSecret, LoginObject.SessionToken);
            IsConnected = true;
            return true;
        }

        /// <summary>
        /// Tries to reconnect your client to the api.
        /// </summary>
        /// <returns>True if successfull else false</returns>
        public bool Reconnect()
        {
            string query =
                $"/my/reconnect?appkey{HttpUtility.UrlEncode(_AppKey)}&sessiontoken={HttpUtility.UrlEncode(LoginObject.SessionToken)}&regaintoken={HttpUtility.UrlEncode(LoginObject.RegainToken)}";
            var response = _ApiHandler.CallServer<LoginObject>(query, LoginObject.ServerEncryptionToken);
            if (response == null)
                return false;

            LoginObject = response;
            LoginObject.ServerEncryptionToken = UpdateEncryptionToken(_LoginSecret, LoginObject.SessionToken);
            LoginObject.DeviceEncryptionToken = UpdateEncryptionToken(_DeviceSecret, LoginObject.SessionToken);
            IsConnected = true;
            return IsConnected;
        }

        /// <summary>
        /// Disconnects the your client from the api
        /// </summary>
        /// <returns>True if successfull else false</returns>
        public bool Disconnect()
        {
            string query = $"/my/disconnect?sessiontoken={HttpUtility.UrlEncode(LoginObject.SessionToken)}";
            var response = _ApiHandler.CallServer<object>(query, LoginObject.ServerEncryptionToken);
            if (response == null)
                return false;

            LoginObject = null;
            return true;
        }
        #endregion
        

        /// <summary>
        /// Lists all Devices which are currently connected to your my.jdownloader.org account.
        /// </summary>
        /// <returns>Returns a list of your currently connected devices.</returns>
        public List<DeviceObject> GetDevices()
        {
            List<DeviceObject> devices = new List<DeviceObject>();
            string query = $"/my/listdevices?sessiontoken={HttpUtility.UrlEncode(LoginObject.SessionToken)}";
            var response = _ApiHandler.CallServer<DeviceJsonReturnObject>(query, LoginObject.ServerEncryptionToken);
            if (response == null)
                return devices;

            foreach (DeviceObject device in response.Devices)
            {
                devices.Add(device);
            }

            return devices;
        }

        /// <summary>
        /// Creates an instance of the DeviceHandler class. 
        /// This is neccessary to call methods!
        /// </summary>
        /// <param name="device">The device you want to call the methods on.</param>
        /// <returns>An deviceHandler instance.</returns>
        public DeviceHandler GetDeviceHandler(DeviceObject device)
        {
            if (IsConnected)
            {
                //TODO: Make it possible to directly connect to the jdownloader client. If it's not working use the relay server.
                //var tmp = _ApiHandler.CallAction<DefaultReturnObject>(device, "/device/getDirectConnectionInfos",
                //    null, LoginObject, true);
                return new DeviceHandler(device, _ApiHandler, LoginObject);
            }
            return null;
        }


        #region "Secret and Encryption tokens"

        private byte[] GetSecret(string email, string password, string domain)
        {
            return EncodeStringToSha256(email.ToLower() + password + domain);
        }

        private readonly SHA256Managed _Sha256Managed = new SHA256Managed();

        private byte[] EncodeStringToSha256(string text)
        {
            return _Sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(text));
        }

        private byte[] UpdateEncryptionToken(byte[] oldToken, string UpdatedToken)
        {
            byte[] newToken = GetByteArrayByHexString(UpdatedToken);
            var newHash = new byte[oldToken.Length + newToken.Length];
            oldToken.CopyTo(newHash, 0);
            newToken.CopyTo(newHash, 32);
            var hashString = new SHA256Managed();
            hashString.ComputeHash(newHash);
            return hashString.Hash;
        }

        private byte[] GetByteArrayByHexString(string hexString)
        {
            hexString = hexString.Replace("-", "");
            byte[] ret = new byte[hexString.Length / 2];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return ret;
        }

        #endregion
    }
}