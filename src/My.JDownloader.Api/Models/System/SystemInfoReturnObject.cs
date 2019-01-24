namespace My.JDownloader.Api.Models.System
{
    public class SystemInfoReturnObject
    {
        public int HeapUsed { get; set; }
        public int HeapCommitted { get; set; }
        public long HeapMax { get; set; }
        public bool Os64Bit { get; set; }
        public bool Arch64Bit { get; set; }
        public long StartupTimeStamp { get; set; }
        public bool Jvm64Bit { get; set; }
        public string ArchFamily { get; set; }
        public string ArchString { get; set; }
        public string OperatingSystem { get; set; }
        public string OsFamily { get; set; }
        public string OsString { get; set; }
        public string JavaVersionString { get; set; }
        public string JavaVendor { get; set; }
        public string JavaName { get; set; }
        public int JavaVersion { get; set; }
        public bool Headless { get; set; }
    }
}
