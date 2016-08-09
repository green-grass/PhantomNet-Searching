using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PhantomNet.Entities;

namespace PhantomNet.Searching.Entities
{
    // Foundation
    public partial class EntitiesSearchService<TEntity, TSearchEntity, TSearchParameters, TSearchResult>
        : EntityManagerBase<TEntity, EntitiesSearchService<TEntity, TSearchEntity, TSearchParameters, TSearchResult>>,
          ISearchService<TSearchEntity, TSearchParameters, TSearchResult>
        where TEntity : class
        where TSearchEntity : class
        where TSearchParameters : class
        where TSearchResult : SearchResult<TSearchEntity>, new()
    {
        #region Constructors

        public EntitiesSearchService(
            IEntitiesSearchProvider<TEntity, TSearchParameters> searchProvider,
            IDisposable store,
            EntityErrorDescriber errors)
            : base(store, null, null, null, null, errors, null)
        {
            if (searchProvider == null)
            {
                throw new ArgumentNullException(nameof(searchProvider));
            }
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            SearchProvider = searchProvider;
        }

        #endregion

        #region Properties

        protected IEntitiesSearchProvider<TEntity, TSearchParameters> SearchProvider { get; }

        #endregion

        #region Public Operations

        public virtual async Task<TSearchResult> SearchAsync(TSearchParameters parameters)
        {
            var entities = Entities;
            var searchDescriptor = SearchProvider.BuildSearchDescriptor(parameters);

            var offset = ((searchDescriptor?.PageNumber - 1) * searchDescriptor?.PageSize) ?? 0;
            var limit = searchDescriptor?.PageSize ?? int.MaxValue;
            var result = await SearchEntitiesInternalAsync(entities, searchDescriptor);
            var filters = await SearchProvider.GetFilters(parameters);

            return new TSearchResult() {
                Total = result.FilterredCount,
                PageNumber = offset + 1,
                PageSize = limit,
                Entities = result.Results.Select(x => Mapper.Map<TSearchEntity>(x)),
                Filters = filters
            };
        }

        public virtual Task<SuggestResult<TSearchEntity, TSearchResult>> SuggestAsync(TSearchParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
