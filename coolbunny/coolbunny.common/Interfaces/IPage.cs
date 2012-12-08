namespace coolbunny.common.Interfaces
{
    public interface IPage
    {
        /// <summary>
        /// The current page number
        /// </summary>
        int? PageNumber { get; set; }
        /// <summary>
        /// The current page size
        /// </summary>
        int? PageSize { get; set; }
        /// <summary>
        /// The sort column
        /// </summary>
        string SortColumn { get; set; }
        /// <summary>
        /// The sort order
        /// </summary>
        string SortOrder { get; set; }
    }
}