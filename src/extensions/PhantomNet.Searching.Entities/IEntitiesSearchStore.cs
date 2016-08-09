using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhantomNet.Searching.Entities
{
    public interface IEntitiesSearchStore<TSearchParameters> : IDisposable
        where TSearchParameters : class
    {
        Task<IEnumerable<IFilter>> GetFilters(TSearchParameters parameters);
    }
}
