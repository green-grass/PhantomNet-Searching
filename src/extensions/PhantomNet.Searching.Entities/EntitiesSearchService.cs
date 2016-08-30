using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace PhantomNet.Searching.Entities
{
    // Foundation
    public partial class EntitiesSearchService<TEntity, TModel, TParameters, TStore, TSearchResult>
        : ISearchService<TModel, TParameters, TSearchResult>
        where TEntity : class
        where TModel : class
        where TParameters : class
        where TStore : IDisposable
        where TSearchResult : SearchResult<TModel>, new()
    {
        #region Constructors

        public EntitiesSearchService(
            IEntitiesSearchProvider<TEntity, TModel, TParameters, TStore> searchProvider,
            TStore store,
            ILogger<EntitiesSearchManager<TEntity, TParameters, TStore>> logger)
            : this(searchProvider)
        {
            Manager = new EntitiesSearchManager<TEntity, TParameters, TStore>(store, logger);
        }

        protected EntitiesSearchService(
            IEntitiesSearchProvider<TEntity, TModel, TParameters, TStore> searchProvider,
            EntitiesSearchManager<TEntity, TParameters, TStore> manager)
            : this(searchProvider)
        {
            Manager = manager;
        }

        private EntitiesSearchService(IEntitiesSearchProvider<TEntity, TModel, TParameters, TStore> searchProvider)
        {
            if (searchProvider == null)
            {
                throw new ArgumentNullException(nameof(searchProvider));
            }

            Provider = searchProvider;
        }

        #endregion

        #region Properties

        protected IEntitiesSearchProvider<TEntity, TModel, TParameters, TStore> Provider { get; }

        protected EntitiesSearchManager<TEntity, TParameters, TStore> Manager { get; }

        #endregion

        #region Public Operations

        public virtual async Task<TSearchResult> SearchAsync(TParameters parameters)
        {
            var searchDescriptor = Provider.BuildSearchDescriptor(parameters);
            IEnumerable<IFilter> presearchFilters = null;
            IEnumerable<IFilter> postsearchFilters = null;
            var result = await Manager.SearchAsync(
                searchDescriptor,
                async (store, entities) => presearchFilters = await Provider.RetrievePresearchFilters(store, entities, parameters),
                async (store, entities) => postsearchFilters = await Provider.RetrievePostsearchFilters(store, entities, parameters));

            var filters = new List<IFilter>(postsearchFilters.Count());
            for (var i = 0; i < postsearchFilters.Count(); i++)
            {
                var postsearchFilter = postsearchFilters.ElementAt(i);

                IFilter filter;
                if (i < presearchFilters.Count())
                {
                    filter = presearchFilters.ElementAt(i);
                    filter.SetOptionsAvailability(postsearchFilter);
                }
                else
                {
                    filter = postsearchFilter;
                }

                filters.Add(filter);
            }

            return new TSearchResult() {
                TotalCount = result.TotalCount,
                FilterredCount = result.FilterredCount,
                PageNumber = searchDescriptor?.PageSize == null ? 1 : searchDescriptor?.PageNumber ?? 1,
                PageSize = searchDescriptor?.PageSize ?? int.MaxValue,
                Matches = result.Results.ToList().Select(x => Mapper.Map<TModel>(x)),
                Filters = filters
            };
        }

        public virtual Task<SuggestResult<TModel, TSearchResult>> SuggestAsync(TParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Support

        private bool _disposed = false;

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Manager.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
