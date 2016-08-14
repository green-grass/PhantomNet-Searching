namespace PhantomNet.Searching
{
    public interface IFilterOption
    {
        bool IsActive { get; set; }

        long MatchCount { get; set; }

        string Group { get; set; }

        string DisplayText { get; }

        string DisplayTextWithMatchCount { get; }
    }
}
