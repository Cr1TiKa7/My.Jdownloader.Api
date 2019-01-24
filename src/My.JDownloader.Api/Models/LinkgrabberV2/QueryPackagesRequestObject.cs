namespace My.JDownloader.Api.Models.LinkgrabberV2
{
    public class QueryPackagesRequestObject
    {
        public bool AvailableOfflineCount { get; set; }
        public bool AvailableOnlineCount { get; set; }
        public bool AvailableTempUnknownCount { get; set; }
        public bool AvailableUnkownCount { get; set; }
        public bool BytesTotal { get; set; }
        public bool ChildCount { get; set; }
        public bool Comment { get; set; }
        public bool Enabled { get; set; }
        public bool Hosts { get; set; }
        public int MaxResults { get; set; }
        public long[] PackageUuids { get; set; }
        public bool Priority { get; set; }
        public bool SaveTo { get; set; }
        public int StartAt { get; set; }
        public bool Status { get; set; }
    }
}
