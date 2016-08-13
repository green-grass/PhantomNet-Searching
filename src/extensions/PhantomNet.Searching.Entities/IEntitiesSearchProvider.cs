using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhantomNet.Entities;

namespace PhantomNet.Searching.Entities
{
    public interface IEntitiesSearchProvider<TEntity, TModel, TParameters, TStore>
        where TEntity : class
        where TModel : class
        where TParameters : class
        where TStore : IDisposable
    {
        IEntitySearchDescriptor<TEntity> BuildSearchDescriptor(TParameters parameters);

        Task<IEnumerable<IFilter>> RetrievePresearchFilters(TStore store, IQueryable<TEntity> entities, TParameters parameters);

        Task<IEnumerable<IFilter>> RetrievePostsearchFilters(TStore store, IQueryable<TEntity> entities, TParameters parameters);
    }
}
