using System;

namespace PhantomNet.Searching
{
    public class CurrencyRangeFilterOption : FilterOptionBase
    {
        public double Start { get; set; }

        public double End { get; set; }

        public string ValueFormat { get; set; } = "C0";

        public Func<double, double, string, string> Formater { get; set; }
            = (start, end, format) => $"{start.ToString(format)} - {end.ToString(format)}";

        public override string DisplayText => Formater(Start, End, ValueFormat);
    }
}
