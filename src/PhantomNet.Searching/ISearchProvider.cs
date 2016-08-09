using System.Collections.Generic;

namespace PhantomNet.Searching
{
    public interface ISearchProvider<TEntity, TSearchParameters>
        where TEntity : class
        where TSearchParameters : class
    {
        IFilter SelectFilter(string filterDisplayText);

        IEnumerable<string> MapParameterNames(string filterDisplayText);

        void ActivateFilterOption(IFilter filter, TSearchParameters parameters);
    }
}
