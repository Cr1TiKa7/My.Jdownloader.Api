namespace My.JDownloader.Api.Models.Captcha.Request
{
    public enum SkipRequest
    {
        SINGLE,
        BLOCK_HOSTER,
        BLOCK_ALL_CAPTCHAS,
        BLOCK_PACKAGE,
        REFRESH,
        STOP_CURRENT_ACTION,
        TIMEOUT
    }
}
