using System;
using System.Security.Cryptography;
using System.Text;

namespace My.JDownloader.Api
{
    internal static class Utils
    {
        internal static string ServerDomain = "server";
        internal static string DeviceDomain = "device";
        internal static string AppKey = "my.jdownloader.api.wrapper";

        #region "Secret and Encryption tokens"

        internal static byte[] GetSecret(string email, string password, string domain)
        {
            return EncodeStringToSha256(email.ToLower() + password + domain);
        }

		internal static readonly SHA256 _sha256 = SHA256.Create();

		internal static byte[] EncodeStringToSha256(string text)
        {
            return _sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
        }

        internal static byte[] UpdateEncryptionToken(byte[] oldToken, string updateToken)
        {
            byte[] newToken = GetByteArrayByHexString(updateToken);
            var newHash = new byte[oldToken.Length + newToken.Length];
            oldToken.CopyTo(newHash, 0);
            newToken.CopyTo(newHash, 32);
            var hashString = SHA256.Create();
			hashString.ComputeHash(newHash);
            return hashString.Hash;
        }

        internal static byte[] GetByteArrayByHexString(string hexString)
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
