using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PhantomNet.Entities;

namespace PhantomNet.Searching.Entities
{
    public class EntitiesSearchManager<TEntity, TParameters, TStore>
        : EntityManagerBase<TEntity, EntitiesSearchManager<TEntity, TParameters, TStore>>
        where TEntity : class
        where TParameters : class
        where TStore : IDisposable
    {
        public EntitiesSearchManager(
            TStore store,
            ILogger<EntitiesSearchManager<TEntity, TParameters, TStore>> logger)
            : base(store, null, null, logger)
        { }

        private TStore EntitiesSearchStore => (TStore)Store;

        protected override bool SupportsEntity
        {
            get
            {
                ThrowIfDisposed();
                return Store is IEntityStore<TEntity>;
            }
        }

        internal Task<EntityQueryResult<TEntity>> SearchAsync(
            IEntitySearchDescriptor<TEntity> searchDescriptor,
            Func<TStore, IQueryable<TEntity>, Task> retrieveFiltersAsync,
            Func<TStore, IQueryable<TEntity>, Task> refineFiltersAsync)
        {
            ThrowIfDisposed();
            if (SupportsQueryableEntity)
            {
                return SearchInternalAsync(QueryableEntityStore.Entities, searchDescriptor, retrieveFiltersAsync, refineFiltersAsync);
            }

            throw new NotImplementedException();
        }

        private async Task<EntityQueryResult<TEntity>> SearchInternalAsync(
            IQueryable<TEntity> entities,
            IEntitySearchDescriptor<TEntity> searchDescriptor,
            Func<TStore, IQueryable<TEntity>, Task> retrieveFiltersAsync,
            Func<TStore, IQueryable<TEntity>, Task> refineFiltersAsync)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (SupportsEagerLoadingEntity)
            {
                entities = EagerLoadingEntityStore.EagerLoad(entities);
            }

            var result = new EntityQueryResult<TEntity>();

            entities = PreFilter(entities, searchDescriptor);
            result.TotalCount = await Count(entities);
            await retrieveFiltersAsync(EntitiesSearchStore, entities);

            entities = Filter(entities, searchDescriptor);
            result.FilteredCount = await Count(entities);
            await refineFiltersAsync(EntitiesSearchStore, entities);

            entities = Sort(entities, searchDescriptor);

            result.Results = Page(entities, searchDescriptor);

            if (SupportsExplicitLoadingEntity)
            {
                result.Results = result.Results.ToList().AsQueryable();
                await ExplicitLoadingEntityStore.ExplicitLoadAsync(result.Results, CancellationToken);
            }

            return result;
        }
    }
}
