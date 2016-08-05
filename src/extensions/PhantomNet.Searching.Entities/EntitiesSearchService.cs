using System;
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
        : ISearchService<TEntity, TSearchParameters, TSearchResult>
        where TEntity : class
        where TSearchParameters : class
        where TSearchResult : SearchResult<TEntity>, new()
    {
        public EntitiesSearchService(IEntitiesSearchProvider<TEntity, TSearchParameters> searchProvider)
        {
            if (searchProvider == null)
            {
                throw new ArgumentNullException(nameof(searchProvider));
            }

            SearchProvider = searchProvider;
        }

        #region Properties

        protected IEntitiesSearchProvider<TEntity, TSearchParameters> SearchProvider { get; }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        protected IDisposable Store { get; }

        protected virtual IQueryable<TEntity> Entities
        {
            get
            {
                if (SupportsQueryableEntity)
                {
                    return QueryableEntityStore.Entities;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        protected virtual bool SupportsEagerLoadingEntity
        {
            get
            {
                ThrowIfDisposed();
                return Store is IEagerLoadingEntityStore<TEntity>;
            }
        }

        protected virtual IEagerLoadingEntityStore<TEntity> EagerLoadingEntityStore
        {
            get
            {
                ThrowIfDisposed();
                var store = Store as IEagerLoadingEntityStore<TEntity>;
                if (store == null)
                {
                    throw new NotSupportedException(Strings.StoreNotIEagerLoadingEntityStore);
                }

                return store;
            }
        }

        protected virtual bool SupportsQueryableEntity
        {
            get
            {
                ThrowIfDisposed();
                return Store is IQueryableEntityStore<TEntity>;
            }
        }

        protected virtual IQueryableEntityStore<TEntity> QueryableEntityStore
        {
            get
            {
                ThrowIfDisposed();
                var store = Store as IQueryableEntityStore<TEntity>;
                if (store == null)
                {
                    throw new NotSupportedException(Strings.StoreNotIQueryableEntityStore);
                }

                return store;
            }
        }

        #endregion

        #region Public Operations

        public virtual async Task<TSearchResult> SearchAsync(TSearchParameters parameters)
        {
            var models = Entities;

            var result = new TSearchResult();
            var offset = (SearchProvider.GetPageNumber(parameters) - 1) * SearchProvider.GetPageSize(parameters);
            var limit = SearchProvider.GetPageSize(parameters);

            // Pre-filter
            models = SearchProvider.PreFilter(models, parameters);

            // Filter
            models = SearchProvider.Filter(models, parameters);
            result.Total = await SearchProvider.CountAsync(models, CancellationToken);

            // Pre-sort
            models = SearchProvider.PreSort(models);

            // Sort
            var sort = SearchProvider.GetSortExpression(parameters);
            var reverse = SearchProvider.GetSortReverse(parameters);
            if (string.IsNullOrWhiteSpace(sort))
            {
                models = SearchProvider.DefaultSort(models);
            }
            else
            {
                if (sort.StartsWith("-"))
                {
                    sort = sort.TrimStart('-');
                    reverse = !reverse;
                }

                var param = Expression.Parameter(typeof(TEntity));
                var propertyInfo = typeof(TEntity).GetProperty(sort);
                var propertyExpression = Expression.Lambda(Expression.Property(param, propertyInfo), param);
                models = (IOrderedQueryable<TEntity>)models.Provider.CreateQuery(Expression.Call(
                    typeof(Queryable),
                    models.Expression.Type == typeof(IOrderedQueryable<TEntity>) ?
                        (reverse ? nameof(Queryable.ThenByDescending) : nameof(Queryable.ThenBy)) :
                        (reverse ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy)),
                    new Type[] { typeof(TEntity), propertyInfo.PropertyType },
                    models.Expression,
                    propertyExpression
                ));
            }

            // Paging
            result.Entities = models.Skip(offset).Take(limit).ToList();

            // Eager loading
            if (SupportsEagerLoadingEntity)
            {
                // Loading all entities parallelly will throw AggregateException exception.
                foreach (var entity in result.Entities)
                {
                    await EagerLoadingEntityStore.EagerLoadAsync(entity, CancellationToken);
                }
            }

            return result;
        }

        public virtual Task<SuggestResult<TEntity, TSearchResult>> SuggestAsync(TSearchParameters parameters)
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
                    Store.Dispose();
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
