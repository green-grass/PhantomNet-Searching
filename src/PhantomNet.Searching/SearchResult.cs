using System.Collections.Generic;

namespace PhantomNet.Searching
{
    public class SearchResult<TModel>
        where TModel : class
    {
        public virtual long TotalCount { get;set;}

        public virtual long FilterredCount { get; set; }

        public virtual int PageNumber { get; set; }

        public virtual int PageSize { get; set; }

        public virtual IEnumerable<TModel> Matches { get; set; }

        public virtual IEnumerable<IFilter> Filters { get; set; }
    }
}
