using PhantomNet.Entities;

namespace PhantomNet.Searching.Entities
{
    public interface IEntitiesSearchProvider<TEntity, TSearchParameters> : ISearchProvider<TEntity, TSearchParameters>
        where TEntity : class
        where TSearchParameters : class
    {
        IEntitySearchDescriptor<TEntity> BuildSearchDescriptor(TSearchParameters parameters);
    }
}
