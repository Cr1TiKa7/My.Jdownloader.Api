using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.AccountV2;
using My.JDownloader.Api.Models.AccountV2.Request;
using My.JDownloader.Api.Models.AccountV2.Response;
using My.JDownloader.Api.Models.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class AccountsV2 : Base
    {

        internal AccountsV2(JDownloaderApiHandler apiHandler, Device device)
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
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/addAccount",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Adds an basic authorization to the client.
        /// </summary>
        /// <param name="request">Contains the needed properties for the request e.g. the username and password.</param>
        /// <returns>True if successfull.</returns>
        public bool AddBasicAuth(BasicAuthRequest request)
        {
            var param = new[]
                {request.Type.ToString(), request.Hostmask, request.Username, request.Password};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/addBasicAuth",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Disables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to disable.</param>
        /// <returns>True if succesfull</returns>
        public bool DisableAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/disableAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Enables an account to download.
        /// </summary>
        /// <param name="accountIds">The account ids you want to enable.</param>
        /// <returns>True if succesfull</returns>
        public bool EnableAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/enableAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Gets a link of a hoster by the name of it.
        /// </summary>
        /// <param name="hoster">Name of the hoster you want the url from.</param>
        /// <returns>The url of the hoster.</returns>
        public string GetPremiumHosterUrl(string hoster)
        {
            var param = new[] {hoster};
            var response = ApiHandler.CallAction<DefaultResponse<string>>(Device, "/accountsV2/getPremiumHosterUrl",
                param, JDownloaderHandler.LoginObject, true);
            if (response?.Data != null)
                return response.Data;
            return "";
        }

        /// <summary>
        /// Lists all accounts which are stored on the device.
        /// </summary>
        /// <param name="request">Contains properties like Username (boolean) etc. If set to true the api will return the Username.</param>
        /// <returns>A list of all accounts stored on the device.</returns>
        public IEnumerable<ListAccountResponse> ListAccounts(ListAccountRequest request)
        {
            string json = JsonConvert.SerializeObject(request);
            var param = new[] {json};
            var response = ApiHandler.CallAction<DefaultResponse<IEnumerable<ListAccountResponse>>>(Device, "/accountsV2/listAccounts", param,
                JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Gets all basic authorization informations of the client.
        /// </summary>
        /// <returns>An enumerable with all basic authorization informations.</returns>
        public IEnumerable<ListBasicAuthResponse> ListBasicAuth()
        {
            var response = ApiHandler.CallAction<DefaultResponse<IEnumerable<ListBasicAuthResponse>>>(Device, "/accountsV2/listBasicAuth", null,
                JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Gets all available premium hoster names of the client.
        /// </summary>
        /// <returns>An enumerable of all available premium hoster names.</returns>
        public IEnumerable<string> ListPremiumHoster()
        {
            var response = ApiHandler.CallAction<DefaultResponse<IEnumerable<string>>>(Device, "/accountsV2/listPremiumHoster", null,
                JDownloaderHandler.LoginObject, true);
            return response?.Data;
        }

        /// <summary>
        /// Gets all premium hoster names + urls that JDownloader supports.
        /// </summary>
        /// <returns>Returns a dictionary containing the hostername as the key and the url as the value.</returns>
        public Dictionary<string, string> ListPremiumHosterUrls()
        {
            var response = ApiHandler.CallAction<DefaultResponse<Dictionary<string, string>>>(Device, "/accountsV2/listPremiumHosterUrls",
                null,
                JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Refreshes all the account informations stored on the device.
        /// </summary>
        /// <param name="accountIds">The account ids you want to refresh.</param>
        /// <returns>True if successfull</returns>
        public bool RefreshAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/refreshAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Removes accounts stored on the device.
        /// </summary>
        /// <param name="accountIds">The account ids you want to remove.</param>
        /// <returns>True if successfull.</returns>
        public bool RemoveAccounts(long[] accountIds)
        {
            var param = new[] {accountIds};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device,"/accountsV2/removeAccounts",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Removes basic auth informations stored on the device.
        /// </summary>
        /// <param name="basicAuthIds">The basic auth ids you want to remove.</param>
        /// <returns>True if successfull.</returns>
        public bool RemoveBasicAuths(long[] basicAuthIds)
        {
            var param = new[] {basicAuthIds};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/removeBasicAuths",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Updates the account data for the given account id.
        /// </summary>
        /// <param name="accountId">The id of the account you want to update.</param>
        /// <param name="email">The old/new email.</param>
        /// <param name="password">The old/new password</param>
        /// <returns>Ture if successfull</returns>
        public bool SetUsernameAndPassword(long accountId, string email, string password)
        {
            var param = new[] {accountId.ToString(), email, password};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/setUserNameAndPassword",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }

        /// <summary>
        /// Updates an basic auth entry.
        /// </summary>
        /// <param name="request">The updated basic auth informations.</param>
        /// <returns>True if successfull.</returns>
        public bool UpdateBasicAuth(BasicAuthRequest request)
        {
            var param = new[] {  JsonConvert.SerializeObject(request)};
            var response = ApiHandler.CallAction<DefaultResponse<object>>(Device, "/accountsV2/updateBasicAuth",
                param, JDownloaderHandler.LoginObject, true);

            return response != null;
        }
    }
}