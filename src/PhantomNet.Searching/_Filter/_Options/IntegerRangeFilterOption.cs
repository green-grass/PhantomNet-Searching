namespace PhantomNet.Searching
{
    public class IntegerRangeFilterOption : FilterOptionBase
    {
        public int Start { get; set; }

        public int End { get; set; }

        public override string DisplayText => $"{Start} - {End}";
    }
}
