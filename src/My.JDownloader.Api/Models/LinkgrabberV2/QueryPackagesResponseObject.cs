namespace My.JDownloader.Api.Models.LinkgrabberV2
{
    public class QueryPackagesResponseObject
    {
        public long BytesTotal { get; set; }
        public int ChildCount { get; set; }
        public string Comment { get; set; }
        public string DownloadPassword { get; set; }
        public bool Enabled { get; set; }
        public string[] Hosts { get; set; }
        public string Name { get; set; }
        public int OfflineCount { get; set; }
        public int OnlineCount { get; set; }
        public PriorityType Priority { get; set; }
        public string SaveTo { get; set; }
        public int TempUnknownCount { get; set; }
        public int UnkownCount { get; set; }
        public long Uuid { get; set; }
    }
}
