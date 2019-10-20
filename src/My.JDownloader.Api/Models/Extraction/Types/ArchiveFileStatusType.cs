namespace My.JDownloader.Api.Models.Extraction.Types
{
    public enum ArchiveFileStatusType
    {
        /// <summary>
        /// File is available for extraction
        /// </summary>
        COMPLETE,
        /// <summary>
        /// File exists, but is incomplete
        /// </summary>
        INCOMPLETE,
        /// <summary>
        /// File does not exist
        /// </summary>
        MISSING
    }
}
