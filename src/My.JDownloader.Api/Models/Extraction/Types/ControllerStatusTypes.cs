namespace My.JDownloader.Api.Models.Extraction.Types
{
    public enum ControllerStatusTypes
    {
        /// <summary>
        /// Extraction is currently running
        /// </summary>
        RUNNING,
        /// <summary>
        /// Archive is queued for extraction and will run as soon as possible
        /// </summary>
        QUEUED,
        /// <summary>
        /// No controller assigned
        /// </summary>
        NA
    }
}
