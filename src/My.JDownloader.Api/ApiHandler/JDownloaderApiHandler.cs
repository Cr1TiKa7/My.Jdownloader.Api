﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using My.JDownloader.Api.Exceptions;
using My.JDownloader.Api.Models.Action;
using My.JDownloader.Api.Models.Devices;
using My.JDownloader.Api.Models.Login;
using Newtonsoft.Json;

namespace My.JDownloader.Api.ApiHandler
{
    public class JDownloaderApiHandler
    {
        private int _requestId = (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        private string _apiUrl = "http://api.jdownloader.org";


        internal JDownloaderApiHandler() { }

        public void SetApiUrl(string newApiUrl)
        {
            _apiUrl = newApiUrl;
        }

        public T CallServer<T>(string query, byte[] key, string param = "")
        {
            string rid;
            if (!string.IsNullOrEmpty(param))
            {
                if (key != null)
                {
                    param = Encrypt(param, key);
                }
                rid = _requestId.ToString();
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

            string url = _apiUrl + query;
            if (!string.IsNullOrWhiteSpace(param))
                param = string.Empty;
            string response = PostMethod(url, param, key);
            if (response == null)
                return default(T);
            return (T) JsonConvert.DeserializeObject(response, typeof(T));
        }

        public T CallAction<T>(Device device, string action, object param, LoginObject loginObject,
            bool decryptResponse = false, bool deserialize = true)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));
            if (string.IsNullOrEmpty(device.Id))
                throw new ArgumentException(
                    "The id of the device is empty. Please call again the GetDevices Method and try again.");

            string query =
                $"/t_{HttpUtility.UrlEncode(loginObject.SessionToken)}_{HttpUtility.UrlEncode(device.Id)}{action}";
            CallAction callAction = new CallAction
            {
                ApiVer = 1,
                Params = param,
                RequestId = GetUniqueRid(),
                Url = action
            };

            //Regex _regex = new Regex("http\\:\\/\\/(192.168.*)\\:37733");
            //var match = _regex.Match(_apiUrl);
            //if (match.Success)
            //{
            //    _apiUrl = _apiUrl.Replace(match.Groups[0].Value, "89.163.144.231");
            //}
            string url = _apiUrl + query;
            //url = url.Replace("172.23.0.8", "89.163.144.231");
            string json = JsonConvert.SerializeObject(callAction);
            json = Encrypt(json, loginObject.DeviceEncryptionToken);
            string response = PostMethod(url,
                json, loginObject.DeviceEncryptionToken);

            if (response != null || !response.Contains(callAction.RequestId.ToString()))
            {
                if (decryptResponse)
                {
                    string tmp = Decrypt(response, loginObject.DeviceEncryptionToken);
                    if (deserialize)
                        return (T) JsonConvert.DeserializeObject(tmp, typeof(T));
                    return (T)Convert.ChangeType(response, typeof(T));
                }
            }
            else
                throw new InvalidRequestIdException("The 'RequestId' differs from the 'RequestId' from the queryRequest.");

            if (deserialize)
                return (T)JsonConvert.DeserializeObject(response, typeof(T));

            return (T)Convert.ChangeType(response, typeof(T));
        }

        private string PostMethod(string url, string body = "", byte[] ivKey = null)
        {
            //url = url.Replace("172.23.0.8", "89.163.144.231");
            //Regex _regex = new Regex("http\\:\\/\\/(192.168.*)\\:37733");
            //var match = _regex.Match(url);
            //if (match.Success)
            //{
            //    url = url.Replace(match.Groups[0].Value, "89.163.144.231");
            //}
            //url = url.Replace("192.168.128.2", "89.163.144.231");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(body))
                    {
                        StringContent content = new StringContent(body, Encoding.UTF8, "application/aesjson-jd");
                        using (var response = httpClient.PostAsync(url, content).Result)
                        {
                            if (response != null)
                            {
                                return response.Content.ReadAsStringAsync().Result;
                            }
                        }
                    }
                    else
                    {
                        using (var response = httpClient.GetAsync(url).Result)
                        {
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
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region "Encrypt, Decrypt and Signature"

        private string GetSignature(string data, byte[] key)
        {
            if (key == null)
            {
                throw new Exception(
                    "The ivKey is null. Please check your login informations. If it's still null the server may has disconnected you.");
            }
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var hmacsha256 = new HMACSHA256(key);
            hmacsha256.ComputeHash(dataBytes);
            var hash = hmacsha256.Hash;
            string binaryString = hash.Aggregate("", (current, t) => current + t.ToString("X2"));
            return binaryString.ToLower();
        }

        private string Encrypt(string data, byte[] ivKey)
        {
            if (ivKey == null)
            {
                throw new Exception(
                    "The ivKey is null. Please check your login informations. If it's still null the server may has disconnected you.");
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
                throw new Exception(
                    "The ivKey is null. Please check your login informations. If it's still null the server may has disconnected you.");
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
            return _requestId++;
        }
    }
}