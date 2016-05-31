using System.Threading.Tasks;

namespace PhantomNet.Searching
{
    public interface ISearchService<TEntity, TSearchParameters, TSearchResult>
        where TEntity : class
        where TSearchParameters : class
        where TSearchResult : SearchResult<TEntity>
    {
        Task<TSearchResult> SearchAsync(TSearchParameters parameters);

        Task<SuggestResult<TEntity, TSearchResult>> SuggestAsync(TSearchParameters parameters);
    }
}
