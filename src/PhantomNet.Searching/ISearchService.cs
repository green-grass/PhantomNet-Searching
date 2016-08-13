using System;
using System.Threading.Tasks;

namespace PhantomNet.Searching
{
    public interface ISearchService<TModel, TParameters, TSearchResult> : IDisposable
        where TModel : class
        where TParameters : class
        where TSearchResult : SearchResult<TModel>
    {
        Task<TSearchResult> SearchAsync(TParameters parameters);

        Task<SuggestResult<TModel, TSearchResult>> SuggestAsync(TParameters parameters);
    }
}
