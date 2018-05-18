using System.Collections.Generic;

namespace My.JDownloader.Api.ApiObjects.AccountV2
{
    public class ListAccountRequestObject
    {
        public bool Enabled { get; set; }
        public int MaxResults { get; set; }
        public int StartAt { get; set; }
        public bool TrafficLeft { get; set; }
        public bool TrafficMax { get; set; }
		[Newtonsoft.Json.JsonProperty(PropertyName = "userName")]
        public bool Username { get; set; }
        public bool Valid { get; set; }
        public bool ValidUntil { get; set; }
    }
}
