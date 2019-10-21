namespace My.JDownloader.Api.Models
{
    public class DefaultResponse<T>
    {
        public T Data { get; set; }
        public object DiffType { get; set; }
        public int RequestId { get; set; }
        public object DiffId { get; set; }
    }
}
