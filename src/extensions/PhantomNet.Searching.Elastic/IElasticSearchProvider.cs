using Nest;

namespace PhantomNet.Searching.Elastic
{
    public interface IElasticSearchProvider<TEntity, TSearchParameters> : ISearchProvider<TEntity, TSearchParameters>
        where TEntity : class
        where TSearchParameters : class
    {
        int GetPageNumber(TSearchParameters parameters);

        int GetPageSize(TSearchParameters parameters);

        ISearchRequest BuildRequest(SearchDescriptor<TEntity> searchDescripter, TSearchParameters parameters);
    }
}
