using My.JDownloader.Api.Models.AccountV2.Types;

namespace My.JDownloader.Api.Models.AccountV2.Request
{
    public class BasicAuthRequest
    {
        public HostType Type { get; set; }
        public string Hostmask { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
