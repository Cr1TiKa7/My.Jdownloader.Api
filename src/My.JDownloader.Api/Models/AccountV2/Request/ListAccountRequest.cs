﻿namespace My.JDownloader.Api.Models.AccountV2.Request
{
    public class ListAccountRequest
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
