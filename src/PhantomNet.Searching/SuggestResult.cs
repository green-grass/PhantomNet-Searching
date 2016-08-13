namespace PhantomNet.Searching
{
    public class SuggestResult<TModel, TSearchResult>
        where TModel : class
        where TSearchResult : SearchResult<TModel>
    {
        public string Suggestions { get; set; }

        public TSearchResult SearchResult { get; set; }
    }
}
