﻿namespace My.JDownloader.Api.Models.Action
{
    public class CallAction
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "params")]
        public object Params { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName ="rid")]
        public int RequestId { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "ApiVer")]
        public int ApiVer { get; set; }
    }
}
