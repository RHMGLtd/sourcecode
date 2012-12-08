using System.Collections.Generic;

namespace coolbunny.common.Helpers
{
    public class PageOfResults<T>
    {
        public int NumberOfPages { get; set; }
        public int CurrentPageNumber { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}