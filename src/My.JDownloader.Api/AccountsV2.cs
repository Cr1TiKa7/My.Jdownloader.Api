using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.ApiObjects.Devices;

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
        /// <param name="device">The device where the account will be added</param>
        /// <param name="hoster">The hoster e.g. mega.co.nz</param>
        /// <param name="username">Your username</param>
        /// <param name="password">Your password</param>
        /// <returns>True if the account was successfully added.</returns>
        public bool AddAccount(DeviceObject device, string hoster, string username, string password)
        {
            var param = new[] { hoster,username,password };
            var response = _ApiHandler.CallAction<object>(device, "/accountsV2/addAccount",
                param, JDownloaderHandler.LoginObject, true);
            if (response == null)
                return false;
            return true;
        }


    }
}
