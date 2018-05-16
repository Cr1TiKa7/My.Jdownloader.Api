using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Account;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.LinkgrabberV2;
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

        #region "linkgrabberV2 calls"
        /// <summary>
        /// Adds a download link to the given device.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <param name="links">The downloadlinks. Seperated by a space.</param>
        /// <param name="packageName">The name of the package.</param>
        /// <param name="priority">The priority of the download. Can be one of the following: HIGHEST, HIGHER, HIGH, DEFAULT, LOW, LOWER, LOWEST</param>
        /// <param name="downloadPassword">The password which may be needed for a download.</param>
        /// <param name="extractPassword">The password if the archive which will be downloaded is locked with.</param>
        /// <param name="autoStart">If true the download starts automatically.</param>
        /// <param name="autoExtract">If true it extracts the downloaded archive after finishing the download.</param>
        public void AddLink(DeviceObject device, string links, string packageName, string destinationFolder, string priority = "DEFAULT", string extractPassword = "", string downloadPassword = "", bool autoStart = true, bool autoExtract = true)
        {
            AddLinkObject linkObject = new AddLinkObject
            {
                Priority = priority,
                Links = links.Replace(" ", "\\r\\n"),
                AutoStart = autoStart,
                PackageName = packageName,
                AutoExtract = autoExtract,
                DownloadPassword = downloadPassword,
                ExtractPassword = extractPassword,
                DestinationFolder = destinationFolder
            };
            string json = JsonConvert.SerializeObject(linkObject);
            var param = new[] { json };
            var response = _HttpClientHandler.CallAction<object>(device, "/linkgrabberv2/addLinks",
                param, _LoginObject);
        }

        /// <summary>
        /// Adds a container to the download list.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <param name="type">I don't know which types are possible.</param>
        /// <param name="content">File as dataurl. https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/Data_URIs </param>
        public void AddContainer(DeviceObject device, string type, string content)
        {
            AddContainerObject containerObject = new AddContainerObject
            {
                Type = type,
                Content = content
            };

            string json = JsonConvert.SerializeObject(containerObject);
            var param = new[] { json };
            var response = _HttpClientHandler.CallAction<object>(device, "/linkgrabberv2/addContainer",
                param, _LoginObject);
        }

        /// <summary>
        /// Clears the linkcollector list.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>True if successfull</returns>
        public bool ClearList(DeviceObject device)
        {
            var response =
                _HttpClientHandler.CallAction<object>(device, "/linkgrabberv2/clearList", null, _LoginObject);
            if (response == null)
                return false;
            return true;

        }

        /// <summary>
        /// Checks how many packages are inside the linkcollector.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>The amount of links which are in the linkcollector.</returns>
        public int GetPackageCount(DeviceObject device)
        {
            var response =
                _HttpClientHandler.CallAction<dynamic>(device, "/linkgrabberv2/getPackageCount", null, _LoginObject, true);
            if (response == null)
                return 0;
            return response.data;
        }

        /// <summary>
        /// Checks if the JDownloader client is still collecting files from links.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>Returns true or false. Depending on if the client is still collecting files.</returns>
        public bool IsCollecting(DeviceObject device)
        {
            var response =
                _HttpClientHandler.CallAction<object>(device, "/linkgrabberv2/isCollection", null, _LoginObject);
            if (response == null)
                return false;
            return true;
        }

        public List<CrawledLinkDataObject> QueryLinks(DeviceObject device, int maxResults = -1)
        {
            QueryLinksObject queryLink = new QueryLinksObject
            {
                Availability = true,
                Url = true
            };
            if (maxResults > 0)
                queryLink.MaxResults = maxResults;

            string json = JsonConvert.SerializeObject(queryLink);
            var param = new[] { json };

            var response =
                _HttpClientHandler.CallAction<CrawledLinkObject>(device, "/linkgrabberv2/queryLinks", param, _LoginObject,true);
            if (response == null)
                return null;
            return response.Data;
        }
        #endregion

        #region "accountsV2 calls"
        /// <summary>
        /// CURRENTLY NOT WORKING! Adds an premium account to your JDownloader device.
        /// </summary>
        /// <param name="device">The device where the account will be added</param>
        /// <param name="hoster">The hoster e.g. mega.co.nz</param>
        /// <param name="username">Your username</param>
        /// <param name="password">Your password</param>
        /// <returns>True if the account was successfully added.</returns>
        public bool AddAccount(DeviceObject device, string hoster, string username, string password)
        {
            AddAccountObject accObject = new AddAccountObject
            {
                PremiumHoster=hoster,
                Username = username,
                Password = password
            };
            string json = JsonConvert.SerializeObject(accObject);
            var param = new[] { json };
            var response = _HttpClientHandler.CallAction<object>(device, "/accountsV2/addAccount",
                param, _LoginObject);
            if (response == null)
                return false;
            return true;
        }

        #endregion
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
