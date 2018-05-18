using System.Collections.Generic;
using System.Linq;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects;
using My.JDownloader.Api.ApiObjects.AccountV2;
using My.JDownloader.Api.ApiObjects.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api
{
    public class AccountsV2
    {
        private readonly JDownloaderApiHandler _ApiHandler;

        internal AccountsV2(JDownloaderApiHandler apiHandler)
        {
            _ApiHandler = apiHandler;
        }

        /// <summary>
        /// Adds an premium account to your JDownloader device.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="hoster">The hoster e.g. mega.co.nz</param>
        /// <param name="email">Your email</param>
        /// <param name="password">Your password</param>
        /// <returns>True if the account was successfully added.</returns>
        public bool AddAccount(DeviceObject device, string hoster, string email, string password)
        {
            var param = new[] { hoster, email, password };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/addAccount",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Enables an account to download.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="accountIds">The account ids you want to enable.</param>
        /// <returns>True if succesfull</returns>
        public bool EnableAccounts(DeviceObject device, long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/enableAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Disables an account to download.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="accountIds">The account ids you want to disable.</param>
        /// <returns>True if succesfull</returns>
        public bool DisableAccounts(DeviceObject device, long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/disableAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Lists all accounts which are stored on the device.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="requestObject">Contains properties like Username (boolean) etc. If set to true the api will return the Username.</param>
        /// <returns>A list of all accounts stored on the device.</returns>
        public ListAccountResponseObject[] ListAccounts(DeviceObject device, ListAccountRequestObject requestObject)
        {
            string json = JsonConvert.SerializeObject(requestObject);
            var param = new[] {json};
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/listAccounts", param,
                JDownloaderHandler.LoginObject,true);
            JArray tmp = (JArray) response.Data;

            return tmp.ToObject<ListAccountResponseObject[]>();
        }

        /// <summary>
        /// Gets all premium hoster names + urls that JDownloader supports.
        /// </summary>
        /// <param name="device">The target device</param>
        /// <returns>Returns a dictionary containing the hostername as the key and the url as the value.</returns>
        public Dictionary<string,string> ListPremiumHosterUrls(DeviceObject device)
        {
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/listPremiumHosterUrls", null,
                JDownloaderHandler.LoginObject, true);
            var tmp = ((JObject)response.Data);
            if (tmp != null)
                return tmp.ToObject<Dictionary<string, string>>();

            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Refreshes all the account informations stored on the device.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="accountIds">The account ids you want to refresh.</param>
        /// <returns>True if successfull</returns>
        public bool RefreshAccounts(DeviceObject device, long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/refreshAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Removes accounts stored on the device.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="accountIds">The account ids you want to remove.</param>
        /// <returns>True if successfull.</returns>
        public bool RemoveAccounts(DeviceObject device, long[] accountIds)
        {
            var param = new[] { accountIds };
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/removeAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Updates the account data for the given account id.
        /// </summary>
        /// <param name="device">The target device.</param>
        /// <param name="accountId">The id of the account you want to update.</param>
        /// <param name="email">The old/new email.</param>
        /// <param name="password">The old/new password</param>
        /// <returns>Ture if successfull</returns>
        public bool SetUsernameAndPassword(DeviceObject device, long accountId, string email, string password)
        {
            var param = new[] {accountId.ToString(), email, password};
            var response = _ApiHandler.CallAction<DefaultReturnObject>(device, "/accountsV2/setUserNameAndPassword",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

    }
}
