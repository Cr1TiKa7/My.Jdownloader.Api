using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.Link;
using My.JDownloader.Api.ApiObjects.Login;
using Newtonsoft.Json;

namespace My.JDownloader.Api
{
    public class JDownloaderHandler
    {

        public bool IsConnected { get; set; } = false;

        private LoginObject _LoginObject;

        private byte[] _LoginSecret;
        private byte[] _DeviceSecret;
        
        private const string _ServerDomain = "server";
        private const string _DeviceDomain = "device";
        private const string _AppKey = "MyJDAPI_CSharp";

        private readonly JDownloaderApiHandler _HttpClientHandler = new JDownloaderApiHandler();
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
            var response = _HttpClientHandler.CallServer<LoginObject>(connectQueryUrl, _LoginSecret);

            //If the response is null the connection was not successfull
            if (response == null)
                return false;

            //Else we are saving the response which contains the SessionToken, RegainToken and the RequestId
            _LoginObject = response;
            _LoginObject.ServerEncryptionToken = UpdateEncryptionToken(_LoginSecret, _LoginObject.SessionToken);
            _LoginObject.DeviceEncryptionToken = UpdateEncryptionToken(_DeviceSecret, _LoginObject.SessionToken);
            return true;
        }

        /// <summary>
        /// Tries to reconnect your client to the api.
        /// </summary>
        /// <returns>True if successfull else false</returns>
        public bool Reconnect()
        {
            string query =
                $"/my/reconnect?appkey{HttpUtility.UrlEncode(_AppKey)}&sessiontoken={HttpUtility.UrlEncode(_LoginObject.SessionToken)}&regaintoken={HttpUtility.UrlEncode(_LoginObject.RegainToken)}";
            var response = _HttpClientHandler.CallServer<LoginObject>(query, _LoginObject.ServerEncryptionToken);
            if (response == null)
                return false;

            _LoginObject = response;
            _LoginObject.ServerEncryptionToken = UpdateEncryptionToken(_LoginSecret, _LoginObject.SessionToken);
            _LoginObject.DeviceEncryptionToken = UpdateEncryptionToken(_DeviceSecret, _LoginObject.SessionToken);
            IsConnected = true;
            return IsConnected;
        }
        /// <summary>
        /// Disconnects the your client from the api
        /// </summary>
        /// <returns>True if successfull else false</returns>
        public bool Disconnect()
        {
            string query = $"/my/disconnect?sessiontoken={ HttpUtility.UrlEncode(_LoginObject.SessionToken)}";
            var response = _HttpClientHandler.CallServer<object>(query, _LoginObject.ServerEncryptionToken);
            if (response == null)
                return false;

            _LoginObject = null;
            return true;
        }
        #endregion

        /// <summary>
        /// Lists all Devices which are currently connected to your my.jdownloader.org account.
        /// </summary>
        /// <returns>Returns a list of your currentyl connected devices.</returns>
        public List<DeviceObject> GetDevices()
        {
            List<DeviceObject> devices = new List<DeviceObject>();
            string query = $"/my/listdevices?sessiontoken={HttpUtility.UrlEncode(_LoginObject.SessionToken)}";
            var response = _HttpClientHandler.CallServer<DeviceJsonReturnObject>(query, _LoginObject.ServerEncryptionToken);
            if (response == null)
                return devices;

            foreach (DeviceObject device in response.Devices)
            {
                devices.Add(device);
            }

            return devices;
        }

        /// <summary>
        /// Adds a download link to the given device.
        /// </summary>
        /// <param name="device">The device where the link gets added.</param>
        /// <param name="link">The downloadlink.</param>
        /// <param name="packageName">The name of the package.</param>
        /// <param name="priority">The priority of the download. Can be one of the following: HIGHEST, HIGHER, HIGH, DEFAULT, LOW, LOWER, LOWEST</param>
        /// <param name="downloadPassword">The password which may be needed for a download.</param>
        /// <param name="extractPassword">The password if the archive which will be downloaded is locked with.</param>
        /// <param name="autoStart">If true the download starts automatically.</param>
        /// <param name="autoExtract">If true it extracts the downloaded archive after finishing the download.</param>
        public void AddLink(DeviceObject device, string link, string packageName, string destinationFolder, string priority = "DEFAULT", string extractPassword = "", string downloadPassword = "", bool autoStart = true, bool autoExtract = true)
        {
            LinkObject linkObject = new LinkObject
            {
                 Priority = priority,
                 Links = link,
                 AutoStart = autoStart,
                 PackageName = packageName,
                 AutoExtract = autoExtract,
                 DownloadPassword = downloadPassword,
                 ExtractPassword = extractPassword,
                 DestinationFolder = destinationFolder
            };
            //dynamic linkObject = new ExpandoObject();
            //linkObject.priority = "DEFAULT";
            //linkObject.links = link;
            //linkObject.autostart = true;
            //linkObject.packageName = packageName;
            string json = JsonConvert.SerializeObject(linkObject);
            var param = new[] {json};
            var result = _HttpClientHandler.CallAction<object>(device, "/linkgrabberv2/addLinks",
                param, _LoginObject);
        }

        #region "Start and stop download process"

        public void StartDownload(DeviceObject device)
        {
            var result = _HttpClientHandler.CallAction<object>(device, "/downloadcontroller/stop", null, _LoginObject);
        }

        public void StopDownload(DeviceObject device)
        {
            var result = _HttpClientHandler.CallAction<object>(device, "/downloadcontroller/start", null, _LoginObject);
        }

        #endregion

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
            byte[] newtoken = FromHex(UpdatedToken);
            var newhash = new byte[oldToken.Length + newtoken.Length];
            oldToken.CopyTo(newhash, 0);
            newtoken.CopyTo(newhash, 32);
            var hashString = new SHA256Managed();
            hashString.ComputeHash(newhash);
            return hashString.Hash;
        }

        private byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
        #endregion  
    }
}
