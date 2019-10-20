using System.Collections.Generic;
using My.JDownloader.Api.Models.LinkgrabberV2.Response;

namespace My.JDownloader.Api.Models.LinkgrabberV2
{
    public class CrawledLink
    {
        public List<QueryLinksResponse> Data { get; set; }
        public object DiffType { get; set; }
        public int RequestId { get; set; }
        public object DiffId { get; set; }
    }
}
