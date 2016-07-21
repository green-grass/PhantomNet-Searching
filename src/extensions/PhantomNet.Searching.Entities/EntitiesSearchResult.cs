namespace PhantomNet.Searching.Entities
{
    public class EntitiesSearchResult<TEntity> : SearchResult<TEntity>
        where TEntity : class
    {
        public long UnfilteredTotal { get; set; }
    }
}
