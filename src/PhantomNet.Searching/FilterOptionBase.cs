namespace PhantomNet.Searching
{
    public abstract class FilterOptionBase : IFilterOption
    {
        public bool IsAvailable { get; set; } = true;

        public bool IsActive { get; set; }

        public long MatchCount { get; set; }

        public string Group { get; set; }

        public abstract string DisplayText { get; }

        public virtual string DisplayTextWithMatchCount => $"{DisplayText} ({MatchCount})";
    }
}
