namespace My.JDownloader.Api.Models.Devices
{
    public class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
