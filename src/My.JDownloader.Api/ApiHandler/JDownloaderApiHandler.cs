using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using My.JDownloader.Api.ApiObjects.Action;
using My.JDownloader.Api.ApiObjects.Devices;
using My.JDownloader.Api.ApiObjects.Login;
using Newtonsoft.Json;

namespace My.JDownloader.Api.ApiHandler
{
    internal class JDownloaderApiHandler
    {
        private int _RequestId =(int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        private string _ApiUrl = "http://api.jdownloader.org";

        public T CallServer<T>(string query, byte[] key, string param = "")
        {
            string rid;
            if (!string.IsNullOrEmpty(param))
            {
                if (key != null)
                {
                    param = Encrypt(param, key);
                }
                rid = _RequestId.ToString();
            }
            else
            {
                rid = GetUniqueRid().ToString();
            }
            if (query.Contains("?"))
                query += "&";
            else
                query += "?";
            query += "rid=" + rid;
            string signature = GetSignature(query, key);
            query += "&signature=" + signature;

            string url = _ApiUrl + query;
            if (!string.IsNullOrWhiteSpace(param))
                param = string.Empty;
            string response = PostMethod(url, param, key);
            if (response == null)
                return default(T);
            return (T)JsonConvert.DeserializeObject(response,typeof(T));
        }

        public T CallAction<T>(DeviceObject device, string action, object param, LoginObject loginObject, bool decryptResponse = false)
        {
            if (device == null)
                throw new ArgumentNullException("The device can't be null.");
            if (string.IsNullOrEmpty(device.Id))
                throw new ArgumentException("The id of the device is empty. Please call again the GetDevices Method and try again.");

            string query =
                $"/t_{HttpUtility.UrlEncode(loginObject.SessionToken)}_{HttpUtility.UrlEncode(device.Id)}{action}";
            CallActionObject callActionObject = new CallActionObject
            {
                 ApiVer = 1,
                 Params = param,
                 RequestId = GetUniqueRid(),
                 Url =action
            };
            
            string url = _ApiUrl + query;
            string json = JsonConvert.SerializeObject(callActionObject);
            json = Encrypt(json, loginObject.DeviceEncryptionToken);
            string response = PostMethod(url,
                json, loginObject.DeviceEncryptionToken);

            if (response == null || !response.Contains(callActionObject.RequestId.ToString()))
            {
                if (decryptResponse)
                {
                    string tmp = Decrypt(response, loginObject.DeviceEncryptionToken);
                    return (T)JsonConvert.DeserializeObject(tmp, typeof(T));
                }
                //TODO: Thorw an InvalidRequestIdException 
                return default(T);
            }
            return (T)JsonConvert.DeserializeObject(response,typeof(T));
        }

        private string PostMethod(string url, string body = "", byte[] ivKey = null)
        {
            using (var httpClient = new HttpClient())
            {
                if (!string.IsNullOrEmpty(body))
                {
                    StringContent content = new StringContent(body, Encoding.UTF8, "application/aesjson-jd");
                    var response = httpClient.PostAsync(url, content).Result;
                    if (response != null)
                    {
                        
                        return response.Content.ReadAsStringAsync().Result;
                    }
                }
                else
                {
                    var response = httpClient.GetAsync(url).Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                        return null;
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (ivKey != null)
                    {
                        result = Decrypt(result, ivKey);
                    }
                    return result;
                }
            }
            return null;
        }
        /// <summary>
        /// Sends a GET method to the JDownloader API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public T GetMethod<T>(string url, Dictionary<string, string> headers)
        {
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                using (HttpResponseMessage response = httpClient.GetAsync(url).Result)
                {
                    string strRet = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(strRet);
                    }

                    return default(T);
                }

            }
        }

        #region "Encrypt, Decrypt and Signature"
        
        private string GetSignature(string data, byte[] key)
        {
            if (key == null)
            {
                throw new Exception("The ivKey is null. Please check your login informations. If it's still null the server may has disconnected you.");
            }
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var hmacsha256 = new HMACSHA256(key);
            hmacsha256.ComputeHash(dataBytes);
            var hash = hmacsha256.Hash;
            string sbinary = hash.Aggregate("", (current, t) => current + t.ToString("X2"));
            return sbinary.ToLower();
        }

        private string Encrypt(string data, byte[] ivKey)
        {
            if (ivKey == null)
            {
                 throw new Exception("The ivKey is null. Please check your login informations. If it's still null the server may has disconnected you.");
            }
            var iv = new byte[16];
            var key = new byte[16];
            for (int i = 0; i < 32; i++)
            {
                if (i < 16)
                {
                    iv[i] = ivKey[i];
                }
                else
                {
                    key[i - 16] = ivKey[i];
                }
            }
            var rj = new RijndaelManaged
            {
                Key = key,
                IV = iv,
                Mode = CipherMode.CBC,
                BlockSize = 128
            };
            ICryptoTransform encryptor = rj.CreateEncryptor();
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(data);
            }
            byte[] encrypted = msEncrypt.ToArray();
            return Convert.ToBase64String(encrypted);
        }
        private string Decrypt(string data, byte[] ivKey)
        {
            if (ivKey == null)
            {
                throw new Exception("The ivKey is null. Please check your login informations. If it's still null the server may has disconnected you.");
            }
            var iv = new byte[16];
            var key = new byte[16];
            for (int i = 0; i < 32; i++)
            {
                if (i < 16)
                {
                    iv[i] = ivKey[i];
                }
                else
                {
                    key[i - 16] = ivKey[i];
                }
            }
            byte[] cypher = Convert.FromBase64String(data);
            var rj = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                IV = iv,
                Key = key
            };
            var ms = new MemoryStream(cypher);
            string result;
            using (var cs = new CryptoStream(ms, rj.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (var sr = new StreamReader(cs))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
        #endregion

        private int GetUniqueRid()
        {
            return _RequestId++;
        }
    }
}
