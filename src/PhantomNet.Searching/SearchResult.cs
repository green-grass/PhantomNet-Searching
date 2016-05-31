using System.Collections.Generic;

namespace PhantomNet.Searching
{
    public class SearchResult<TEntity>
        where TEntity : class
    {
        public virtual long Total { get; set; }

        public virtual int PageNumber { get; set; }

        public virtual int PageSize { get; set; }

        public virtual ICollection<TEntity> Entities { get; set; }

        public virtual ICollection<IFilter> Filters { get; set; }
    }
}
