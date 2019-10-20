using Newtonsoft.Json;

namespace My.JDownloader.Api.Models.Captcha.Response
{
    public class CaptchaJobResponse
    {
        [JsonProperty(PropertyName = "captchaCategory")]
        public string CaptchaCategory{ get; set; }
        [JsonProperty(PropertyName = "created")]
        public long Created{ get; set; }
        [JsonProperty(PropertyName = "explain")]
        public string Explain{ get; set; }
        [JsonProperty(PropertyName = "hoster")]
        public string Hoster{ get; set; }
        [JsonProperty(PropertyName = "id")]
        public long Id{ get; set; }
        [JsonProperty(PropertyName = "link")]
        public long Link { get; set; }
        [JsonProperty(PropertyName = "timeout")]
        public int Timeout { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
