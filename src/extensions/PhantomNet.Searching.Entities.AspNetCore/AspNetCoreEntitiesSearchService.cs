using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PhantomNet.Searching.Entities.AspNetCore
{
    public class AspNetCoreEntitiesSearchService<TEntity, TModel, TParameters, TStore, TSearchResult>
        : EntitiesSearchService<TEntity, TModel, TParameters, TStore, TSearchResult>
        where TEntity : class
        where TModel : class
        where TParameters : class
        where TStore : IDisposable
        where TSearchResult : SearchResult<TModel>, new()
    {
        public AspNetCoreEntitiesSearchService(
            IEntitiesSearchProvider<TEntity, TModel, TParameters, TStore> searchProvider,
            TStore store,
            ILogger<EntitiesSearchManager<TEntity, TParameters, TStore>> logger,
            IHttpContextAccessor contextAccessor)
            : base(searchProvider, new AspNetCoreEntitiesSearchManager<TEntity, TParameters, TStore>(store, logger, contextAccessor))
        { }
    }
}
