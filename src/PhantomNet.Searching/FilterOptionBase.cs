namespace PhantomNet.Searching
{
    public abstract class FilterOptionBase : IFilterOption
    {
        public virtual bool IsAvailable { get; set; } = true;

        public virtual bool IsActive { get; set; }

        public virtual long EntityCount { get; set; }

        public virtual string Group { get; set; }

        public abstract string DisplayText { get; }

        public virtual string DisplayTextWithEntityCount => $"{DisplayText} ({EntityCount})";
    }
}
