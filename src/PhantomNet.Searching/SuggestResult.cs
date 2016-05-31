namespace PhantomNet.Searching
{
    public class SuggestResult<TEntity, TSearchResult>
        where TEntity : class
        where TSearchResult : SearchResult<TEntity>
    {
        public string Suggestions { get; set; }

        public TSearchResult SearchResult { get; set; }
    }
}
