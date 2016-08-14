namespace PhantomNet.Searching.Mvc
{
    public interface IFilterOptionViewModel
    {
        bool IsAvailable { get; set; }

        bool IsActive { get; set; }

        long MatchCount { get; set; }

        string Group { get; set; }

        string DisplayText { get; set; }

        string DisplayTextWithMatchCount { get; set; }
    }

    public class FilterOptionViewModel
    {
        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

        public long MatchCount { get; set; }

        public string Group { get; set; }

        public string DisplayText { get; set; }

        public string DisplayTextWithMatchCount { get; set; }
    }
}
