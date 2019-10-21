using System.Collections.Generic;
using My.JDownloader.Api.ApiHandler;
using My.JDownloader.Api.Models;
using My.JDownloader.Api.Models.Captcha;
using My.JDownloader.Api.Models.Captcha.Request;
using My.JDownloader.Api.Models.Captcha.Response;
using My.JDownloader.Api.Models.Devices;
using Newtonsoft.Json.Linq;

namespace My.JDownloader.Api.Namespaces
{
    public class Captcha : Base
    {
        internal Captcha(JDownloaderApiHandler apiHandler, Device device)
        {
            ApiHandler = apiHandler;
            Device = device;
        }

        /// <summary>
        /// Gets the captcha by the given id
        /// </summary>
        /// <param name="id">The id of the captcha</param>
        /// <returns>An base64 encoded data url</returns>
        public string Get(long id)
        {
            var param = new[] {id};
            var response = ApiHandler.CallAction<DefaultResponse<string>>(Device, "/captcha/get",
                param, JDownloaderHandler.LoginObject, true);
            if (response?.Data != null)
                return response.Data;
            return "";
        }

        /// <summary>
        /// Gets the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="format">This parameter was not described in the official api documentation.</param>
        /// <returns>An base64 encoded data url.</returns>
        public string Get(long id, string format)
        {
            var param = new object[] {id, format};
            var response = ApiHandler.CallAction<DefaultResponse<string>>(Device, "/captcha/get",
                param, JDownloaderHandler.LoginObject, true);
            if (response?.Data != null)
                return response.Data;
            return "";
        }

        /// <summary>
        /// Gets informations about a specific captcha job.
        /// </summary>
        /// <param name="id">The id of the captcha job.</param>
        /// <returns>An object which contains the informations about the captcha job.</returns>
        public CaptchaJobResponse GetCaptchaJob(long id)
        {
            var param = new[] {id};
            var response = ApiHandler.CallAction<DefaultResponse<CaptchaJobResponse>>(Device, "/captcha/getCaptchaJob",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Gets all available captcha jobs.
        /// </summary>
        /// <returns>An enumerable which contains informations about all available captcha jobs.</returns>
        public IEnumerable<CaptchaJobResponse> List()
        {
            var response = ApiHandler.CallAction<DefaultResponse<IEnumerable<CaptchaJobResponse>>>(Device, "/captcha/list",
                null, JDownloaderHandler.LoginObject, true);

            return response?.Data;
        }

        /// <summary>
        /// Skips the captcha job by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha job.</param>
        /// <param name="type">The skip type. Like block all captchas for the package.</param>
        /// <returns>True if successful.</returns>
        public bool Skip(long id, SkipRequest type)
        {
            var param = new[] {id};
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/captcha/skip",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data;
        }

        /// <summary>
        /// Solves the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="result">The result of the captcha.</param>
        /// <param name="resultFormat">The format of the result.</param>
        /// <returns>True if successful.</returns>
        public bool Solve(long id, string result, string resultFormat)
        {
            var param = new object[] { id, result, resultFormat };
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/captcha/solve",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data;
        }

        /// <summary>
        /// Solves the captcha by the given id.
        /// </summary>
        /// <param name="id">The id of the captcha.</param>
        /// <param name="result">The result of the captcha.</param>
        /// <returns>True if successful.</returns>
        public bool Solve(long id, string result)
        {
            var param = new object[] { id, result };
            var response = ApiHandler.CallAction<DefaultResponse<bool>>(Device, "/captcha/solve",
                param, JDownloaderHandler.LoginObject, true);

            return response?.Data != null && response.Data;
        }
    }
}