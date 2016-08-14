using System.Collections.Generic;

namespace PhantomNet.Searching
{
    public class SearchResult<TModel>
        where TModel : class
    {
        public long TotalCount { get; set; }

        public long FilterredCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<TModel> Matches { get; set; }

        public IEnumerable<IFilter> Filters { get; set; }
    }
}
