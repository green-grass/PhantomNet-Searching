using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhantomNet.Searching.Entities
{
    public interface IEntitiesSearchProvider<TEntity, TSearchParameters> : ISearchProvider<TEntity, TSearchParameters>
        where TEntity : class
        where TSearchParameters : class
    {
        string GetSortExpression(TSearchParameters parameters);

        bool GetSortReverse(TSearchParameters parameters);

        IQueryable<TEntity> PreFilter(IQueryable<TEntity> query, TSearchParameters parameters);

        IQueryable<TEntity> Filter(IQueryable<TEntity> query, TSearchParameters parameters);

        IQueryable<TEntity> PreSort(IQueryable<TEntity> query);

        IQueryable<TEntity> DefaultSort(IQueryable<TEntity> query);

        Task<int> CountAsync(IQueryable<TEntity> entities, CancellationToken cancellationToken);
    }
}
