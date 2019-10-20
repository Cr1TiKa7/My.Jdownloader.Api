namespace My.JDownloader.Api.Models.System.Response
{
    public class StorageInfoResponse
    {
        public string Path { get; set; }
        public object Error { get; set; }
        public long Size { get; set; }
        public long Free { get; set; }
    }
}
