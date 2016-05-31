namespace PhantomNet.Searching
{
    public class CurrencyRangeFilterOption : FilterOptionBase
    {
        public double Start { get; set; }

        public double End { get; set; }

        public string Format { get; set; } = "C0";

        public override string DisplayText => $"{Start.ToString(Format)} - {End.ToString(Format)}";
    }
}
