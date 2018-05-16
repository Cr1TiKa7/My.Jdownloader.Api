using System;
using System.Collections.Generic;
namespace My.JDownloader.Api.ApiObjects.Account
{
    public class AddAccountObject
    {
        [Newtonsoft.Json.JsonProperty(PropertyName ="premiumHoster")]
        public string PremiumHoster { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
