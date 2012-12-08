using coolbunny.common.Interfaces;

namespace coolbunny.common.Helpers
{
    public class PagingParams
    {
        public PagingParams(int pageNumber, int pageSize, string sortColumn, string sortOrder)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }

        public PagingParams(IPage url, string sortColumn)
        {
            PageNumber = url.PageNumber ?? 1;
            PageSize = url.PageSize ?? 50;
            SortColumn = sortColumn;
            SortOrder = string.IsNullOrWhiteSpace(url.SortOrder) ? "asc" : url.SortOrder;
        }

        public PagingParams(IPage url)
        {
            PageNumber = url.PageNumber ?? 1;
            PageSize = url.PageSize ?? 50;
            SortColumn = url.SortColumn;
            SortOrder = string.IsNullOrWhiteSpace(url.SortOrder) ? "asc" : url.SortOrder;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}