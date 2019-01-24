using System.Collections.Generic;

namespace My.JDownloader.Api.Models.LinkgrabberV2
{
    public class CrawledLinkObject
    {
        public List<QueryLinksResponseObject> Data { get; set; }
        public object DiffType { get; set; }
        public int RequestId { get; set; }
        public object DiffId { get; set; }
    }
}
