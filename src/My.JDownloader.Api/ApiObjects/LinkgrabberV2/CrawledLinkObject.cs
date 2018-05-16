using System.Collections.Generic;

namespace My.JDownloader.Api.ApiObjects.LinkgrabberV2
{
    public class CrawledLinkObject
    {
        public List<CrawledLinkDataObject> Data { get; set; }
        public object DiffType { get; set; }
        public int RequestId { get; set; }
        public object DiffId { get; set; }
    }
}
