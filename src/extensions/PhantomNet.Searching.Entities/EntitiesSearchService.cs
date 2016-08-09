using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using PhantomNet.Entities;

namespace PhantomNet.Searching.Entities
{
    // Foundation
    public partial class EntitiesSearchService<TEntity, TSearchParameters, TSearchResult>
        : EntityManagerBase<TEntity, EntitiesSearchService<TEntity, TSearchParameters, TSearchResult>>,
          ISearchService<TEntity, TSearchParameters, TSearchResult>
        where TEntity : class
        where TSearchParameters : class
        where TSearchResult : SearchResult<TEntity>, new()
    {
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

            var searchResult = new TSearchResult() {
                Total = result.FilterredCount,
                PageNumber = offset + 1,
                PageSize = limit,
                Entities = result.Results.ToList()
            };

            // TODO:: Filters

            return searchResult;
        }

        public virtual Task<SuggestResult<TEntity, TSearchResult>> SuggestAsync(TSearchParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
