namespace PhantomNet.Searching
{
    public interface IFilterOption
    {
        bool IsActive { get; set; }

        long EntityCount { get; set; }

        string Group { get; set; }

        string DisplayText { get; }

        string DisplayTextWithEntityCount { get; }
    }
}
