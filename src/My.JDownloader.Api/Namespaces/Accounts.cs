using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Accounts;
using My.JDownloader.Api.Models.AccountV2;
using My.JDownloader.Api.Models.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class Accounts : Base
    {
        internal Accounts(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Adds an premium account to your JDownloader device.
        /// </summary>
        /// <param name="hoster">The hoster e.g. mega.co.nz</param>
        /// <param name="email">Your email</param>
        /// <param name="password">Your password</param>
        /// <returns>True if the account was successfully added.</returns>
        public bool AddAccount(string hoster, string email, string password)
        {
            var param = new[] {hoster, email, password};
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/accounts/addAccount",
                param, JDownloaderHandler.LoginObject, true);

            if (response?.Data == null) return false;

            return response.Data;
        }

        /// <summary>
        /// Disables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to disable.</param>
        /// <returns>True if succesful</returns>
        public bool DisableAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/accounts/disableAccounts",
                param, JDownloaderHandler.LoginObject, true);

            if (response?.Data == null) return false;

            return response.Data;
        }

        /// <summary>
        /// Enables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to enable.</param>
        /// <returns>True if succesful</returns>
        public bool EnableAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/accounts/enableAccounts",
                param, JDownloaderHandler.LoginObject, true);

            if (response?.Data == null) return false;

            return response.Data;
        }

        /// <summary>
        /// Gets information about an account
        /// </summary>
        /// <param name="accountId">The id of the account you want to check</param>
        /// <returns>An object which contains the informations about the account.</returns>
        public Account GetAccountInfo(long accountId)
        {
            var param = new[] {accountId};
            var response = ApiHandler.CallAction<DefaultResponse<Account>>(Device, "/accounts/getAccountInfo",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Gets a link of a hoster by the name of it.
        /// </summary>
        /// <param name="hoster">Name of the hoster you want the url from.</param>
        /// <returns>The url of the hoster.</returns>
        public string GetPremiumHosterUrl(string hoster)
        {
            var param = new[] {hoster};
            var response = ApiHandler.CallAction<DefaultResponse<string>>(Device, "/accounts/getPremiumHosterUrl",
                param, JDownloaderHandler.LoginObject, true);
            if (response?.Data != null)
                return response.Data.ToString();
            return "";
        }

        /// <summary>
        /// Gets all available premium hoster names of the client.
        /// </summary>
        /// <returns>An enumerable of all available premium hoster names.</returns>
        public IEnumerable<string> ListPremiumHoster()
        {
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accounts/listPremiumHoster", null,
                JDownloaderHandler.LoginObject, true);
            var tmp = ((JArray) response.Data);
            return tmp?.ToObject<IEnumerable<string>>();
        }

        /// <summary>
        /// Gets all premium hoster names + urls that JDownloader supports.
        /// </summary>
        /// <returns>Returns a dictionary containing the hostername as the key and the url as the value.</returns>
        public Dictionary<string, string> ListPremiumHosterUrls()
        {
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accounts/listPremiumHosterUrls",
                null,
                JDownloaderHandler.LoginObject, true);
            var tmp = ((JObject) response.Data);
            if (tmp != null)
                return tmp.ToObject<Dictionary<string, string>>();

            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets all premium hoster names + urls that JDownloader supports.
        /// </summary>
        /// <returns>Returns a dictionary containing the hostername as the key and the url as the value.</returns>
        public object PremiumHosterIcon(string premiumHoster)
        {
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accounts/premiumHosterIcon",
                new[] {premiumHoster},
                JDownloaderHandler.LoginObject, true);
          
            return response;
        }

        /// <summary>
        /// Queries all accounts.
        /// </summary>
        /// <param name="query">The queryRequest settings.</param>
        /// <returns>An enumerable which contains all accounts.</returns>
        public IEnumerable<Account> QueryAccounts(ApiQuery query)
        {
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accounts/queryAccounts",
                null,
                JDownloaderHandler.LoginObject, true);

            JArray tmp = (JArray) response.Data;

            return tmp.ToObject<IEnumerable<Account>>();
        }

        /// <summary>
        /// Removes accounts stored on the device.
        /// </summary>
        /// <param name="accountIds">The account ids you want to remove.</param>
        /// <returns>True if successfull.</returns>
        public bool RemoveAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/accounts/removeAccounts",
                param, JDownloaderHandler.LoginObject, true);

            if (response?.Data == null) return false;

            return response.Data;
        }

        /// <summary>
        /// Set the enabled status for the accounts
        /// </summary>
        /// <param name="enabled">True if you want to enable the accounts.</param>
        /// <param name="accountIds">The account ids you want to enabled/disable</param>
        /// <returns>True if successful</returns>
        public bool SetEnabledState(bool enabled, long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/accounts/setEnabledState",
                param, JDownloaderHandler.LoginObject, true);

            if (response?.Data == null) return false;

            return response.Data;
        }
        
        /// <summary>
        /// Updates the username and password for the given account id.
        /// </summary>
        /// <param name="accountId">The id of the account you want to update.</param>
        /// <param name="username">The new username.</param>
        /// <param name="password">The new password.</param>
        /// <returns>True if successful</returns>
        public bool UpdateAccount(long accountId, string username, string password)
        {
            var param = new object[] { accountId,username,password };
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/accounts/updateAccount",
                param, JDownloaderHandler.LoginObject, true);

            if (response?.Data == null) return false;

            return response.Data;
        }
    }
}